using System;

public class Game{
    public int CurrentPlayerTurn;
    public int PlayerCount;
    public int BoxCount;
    public Box[] ArrayBox;
    public Player[] ArrayPlayer;
    public ScriptGameManager manager;
    
    public static int STARTING_AMOUNT = 1000;
    public static int INCENTIVES_AFTER_ROUND_COMPLETION = 500;
    public static double OFF_ON_PURCHASE_IF_CORRECT_ANSWER_GIVEN = 0.5;
    public static double OFF_ON_RENT_IF_CORRECT_ANSWER_GIVEN = 0.5;
    public static int PRISON_FINE = 500;
    public static double INCOME_TAX_PERCENTAGE = 0.3;
    public static int MAX_INCOME_TAX_CONCESSION = 1000;
    public static int HOTEL_FINE = 1000;

    public Game(Player[] ArrayPlayer, Box[] ArrayBox, ScriptGameManager manager){
        this.ArrayPlayer = ArrayPlayer;
        this.ArrayBox = ArrayBox;
        this.manager = manager;
        this.BoxCount = 28;
        this.CurrentPlayerTurn = 0;
        this.PlayerCount = ArrayPlayer.Length;
        for (int i = 0; i < PlayerCount; i++)
            ArrayPlayer[i].allotMoney(STARTING_AMOUNT);
    }

    public void MovePlayer(int count){
        Player currentPlayer = ArrayPlayer[CurrentPlayerTurn];
        int currentPlayerOldPosition = currentPlayer.getPosition();
        int currentPlayerNewPosition = (currentPlayerOldPosition + count) % BoxCount;
        Box currentBox = ArrayBox[currentPlayerNewPosition];
        bool roundCompleted = false;
        currentPlayer.setPosition(currentPlayerNewPosition);

        if(currentPlayerNewPosition < currentPlayerOldPosition){
            roundCompleted = true;
            currentPlayer.allotMoney(INCENTIVES_AFTER_ROUND_COMPLETION);            
        }

        if(currentBox.Ticket){
            if(currentBox.PlayerId == -1){
                manager.MovePlayerResult(currentPlayer.Id, currentPlayerOldPosition, currentPlayerNewPosition, false,
                        true, false, false, false, roundCompleted);
            }else if(currentBox.PlayerId != currentPlayer.Id){
                manager.MovePlayerResult(currentPlayer.Id, currentPlayerOldPosition, currentPlayerNewPosition, true,
                        false, false, false, false, roundCompleted);           
            }else{
                PassTurn();
            }
        } else {
            if (currentPlayerNewPosition == Box.PRISON_TILE){
                manager.MovePlayerResult(currentPlayer.Id, currentPlayerOldPosition, currentPlayerNewPosition, false,
                        false, true, false, false, roundCompleted);
            }else if (currentPlayerNewPosition == Box.INCOME_TAX_TILE){
                manager.MovePlayerResult(currentPlayer.Id, currentPlayerOldPosition, currentPlayerNewPosition, false,
                        false, false, true, false, roundCompleted);
            }else if (currentPlayerNewPosition == Box.HOTEL_TILE){
                manager.MovePlayerResult(currentPlayer.Id, currentPlayerOldPosition, currentPlayerNewPosition, false,
                        false, false, false, true, roundCompleted);
            }else{
                PassTurn();
            }
        }
    }

    public void PassTurn(){
        int i = 1;
        while (i < PlayerCount){
            if(ArrayPlayer[(i + CurrentPlayerTurn) % PlayerCount].isActive){
                CurrentPlayerTurn = (i + CurrentPlayerTurn) % PlayerCount ;
                manager.PassTurnResult();
                return;
            }
            else
                i++;
        }
        manager.GameEndResult();
    }

    public void BuyCity(bool isCorrectAnswerGiven){
        int currentPlayerPosition = ArrayPlayer[CurrentPlayerTurn].getPosition();
        int costOfCurrentCity = ArrayBox[currentPlayerPosition].getCost();
        if (isCorrectAnswerGiven)
            costOfCurrentCity =(int)Math.Ceiling( costOfCurrentCity * OFF_ON_PURCHASE_IF_CORRECT_ANSWER_GIVEN);
        bool isPossible = ArrayPlayer[CurrentPlayerTurn].checkMoneyAvailability(costOfCurrentCity);
        if(isPossible){
            ArrayBox[currentPlayerPosition].buyCity(CurrentPlayerTurn);
            ArrayPlayer[CurrentPlayerTurn].deductMoney(costOfCurrentCity);
        }
        manager.BuyCityResult(isPossible);
    }
    
    public void GiveCityRent(bool isCorrectAnswerGiven){
        bool isPossible = true;
        int currentPlayerPosition = ArrayPlayer[CurrentPlayerTurn].getPosition();
        int actualRentOfCurrentCity = ArrayBox[currentPlayerPosition].getRent();
        int rentOfCurrentCity = actualRentOfCurrentCity ;
        if (isCorrectAnswerGiven)
            rentOfCurrentCity=(int)Math.Ceiling(rentOfCurrentCity*OFF_ON_RENT_IF_CORRECT_ANSWER_GIVEN);
        isPossible = ArrayPlayer[CurrentPlayerTurn].checkMoneyAvailability(rentOfCurrentCity);
        if (isPossible){
            ArrayPlayer[CurrentPlayerTurn].deductMoney(rentOfCurrentCity);
            ArrayPlayer[ArrayBox[currentPlayerPosition].PlayerId].allotMoney(actualRentOfCurrentCity);
        }
        manager.GiveCityRentResult(isPossible);
    }

    //Called
    public void MortageCity(int cityIndex){
        //checking if city belong to current player and loan is  not taken
        bool isPossible = (!ArrayBox[cityIndex].isLoanTaken()) && (CurrentPlayerTurn == ArrayBox[cityIndex].getPlayerId());
        if(isPossible)
        {
            ArrayBox[cityIndex].takeLoan();
            ArrayPlayer[CurrentPlayerTurn].allotMoney(ArrayBox[cityIndex].bankLoanAmmount());
        }
        //manager.MortageCity(bool )
        // manager.MortageCity(isPossible);
    }

    //Called
    public void DeMortageCity(int cityIndex)
    {
        //checking if city belong to current player and loan is already  taken
        bool isPossible = (ArrayBox[cityIndex].isLoanTaken()) && (CurrentPlayerTurn == ArrayBox[cityIndex].getPlayerId());

        int moneyToDemortageCity = ArrayBox[cityIndex].getCost();
        // checkiing if enough money is present
        isPossible = isPossible && ArrayPlayer[CurrentPlayerTurn].checkMoneyAvailability(moneyToDemortageCity);
        if (isPossible)
        {
            ArrayBox[cityIndex].returnLoan();
            ArrayPlayer[CurrentPlayerTurn].deductMoney(moneyToDemortageCity);
        }
        //manager.DeMortageCity(bool )
        // manager.DeMortageCity(isPossible);
    }

    //Called
    public void PlayerGameOver(){
        ArrayPlayer[CurrentPlayerTurn].deactivatePlayer();
        for(int i = 0; i < 28; i++){
            if (ArrayBox[i].getPlayerId() == CurrentPlayerTurn)
                ArrayBox[i].resetCity();
        }
        manager.PassTurnResult();
    }

    public void PayPrisonFine(){
        bool isPossible = ArrayPlayer[CurrentPlayerTurn].checkMoneyAvailability(PRISON_FINE);
        if (isPossible)
            ArrayPlayer[CurrentPlayerTurn].deductMoney(PRISON_FINE);
        manager.PayPrisonFineResult(isPossible);
    }

    public void PayIncomeTax(){
        Player currentPlayer = ArrayPlayer[CurrentPlayerTurn];
        if(currentPlayer.checkMoneyAvailability(MAX_INCOME_TAX_CONCESSION)){
            int taxToPay = (int)Math.Floor((ArrayPlayer[CurrentPlayerTurn].getMoneyInHand() - MAX_INCOME_TAX_CONCESSION) * INCOME_TAX_PERCENTAGE);
            currentPlayer.deductMoney(taxToPay);
        }
        manager.PayIncomeTaxResult(true);
    }

    public void PayHotelFine(){
        bool isPossible = ArrayPlayer[CurrentPlayerTurn].checkMoneyAvailability(HOTEL_FINE);
        if (isPossible)
            ArrayPlayer[CurrentPlayerTurn].deductMoney(HOTEL_FINE);
        manager.PayHotelFineResult(isPossible);
    }

    public void BoxType(int BoxPosition){
        Box box = ArrayBox[BoxPosition];
        //BoxTypeResult(bool CanSellOrMortage, bool CanDemortage, bool CanBuy, bool CanPayRent,
          //       bool CanPayHotelFine, bool CanPayIncomeTaxFine, bool CanPayPrisonFine){

        
    }
}