public class Game {
    public int CurrentPlayerTurn;
    public int PlayerCount;
    public int BoxCount;
    public Box[] ArrayBox ;
    public Player[] ArrayPlayer;

    public Game(Player[] ArrayPlayer, Box[] ArrayBox){
        //Init Array Box
        //Init Variables
        //Set Position To 0
        //Allot Money To Players
    }

    public void MovePlayer(int count){
        //Change Current Player Position

        //On Player Moved
    }

    public void PassTurn(){

    }

    public void GetAmountOnRoundCompletion(){
        //Increment Amount
    }

    public bool HasEnoughAmountToBuyCity(){
        //Edit Here
        return true;
    }

     public bool HasEnoughAmountToPayRent(){
        //Edit Here
        return true;
    }

    public void BuyCity(){
        //Change City Object Status
        //Reduce Player Money
    }

    public void GiveCityRent(){

    }

    public void SellCity(){

    }

    public void BankMortageCity(){

    }

    public void PayJailFine(){

    }

    public void GetRestaurantMoney(){

    }

    public void PayIncomeTax(){

    }

    public void ExitPlayer(){
        
    }

}
