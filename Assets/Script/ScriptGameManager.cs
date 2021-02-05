using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptGameManager : MonoBehaviour {

    public GameObject panelExit, panelBuyCity, panelPayRent, panelQuestion, panelAmount,
             panelHotel, panelPrison, panelIncomeTax, panelInsufficientAmount;
    public Transform transformDice1, transformDice2;
    public Button buttonDice1, buttonDice2, buttonExit, buttonStats, buttonBuy, buttonGoNext, buttonBuyClose, buttonPayRent,
                buttonQuizOption1, buttonQuizOption2, buttonQuizOption3, buttonQuizOption4, buttonExitYes, buttonExitNo; 
    public Sprite dice1, dice2, dice3, dice4, dice5, dice6;
    public Transform[] transformPlayer;

    private double[] ArrayCenterPositionX = {3.5, 2.5, 1.5, 0.5, -0.5, -1.5, -2.5, -3.5, -3.5, -3.5, -3.5, -3.5, -3.5, -3.5,
                     -3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5, 3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 3.5};
    private double[] ArrayCenterPositionZ = {-3.5, -3.5, -3.5, -3.5, -3.5, -3.5, -3.5, -3.5, -2.5, -1.5, -0.5, 0.5, 1.5, 2.5,
                     3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 3.5, 2.5, 1.5, 0.5, -0.5, -1.5, -2.5};
    private double CenterPositionY = 0.25;
    private float TimeDiceRoll;
    private bool Answered;
    private bool CorrectAnswered;
    private Game game ;
    
    void Start(){
        //Init Variables
        //Init Board UI
        buttonBuy.onClick.AddListener(OnBuyClicked);
        buttonBuyClose.onClick.AddListener(OnBuyCloseClicked);
        buttonGoNext.onClick.AddListener(OnGoNextClicked);
        transformDice1.GetComponent<Button>().onClick.AddListener(OnDiceClicked);
        transformDice2.GetComponent<Button>().onClick.AddListener(OnDiceClicked);
        game = new Game(InitArrayPlayer(), InitArrayBox(), this);
    }

   
    Box[] InitArrayBox(){
        //Edit Here
        Box[] box = new Box[28];
        for(int i=0 ; i<28 ; i++)
        box[i] = new Box(i,1000,200,"Mumbai");
        return box;
    }

    Player[] InitArrayPlayer(){
        ScriptHomeScene.playerCount = 4;
        Player[] player = new Player[ScriptHomeScene.playerCount];
        for(int i=0; i<ScriptHomeScene.playerCount ; i++ )
            player[i] = new Player(i, "ScriptHomeScene.namePlayer[i]");
        return player;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            OnExitClicked();
        }
        if(TimeDiceRoll > 0.0f){
            RollDice();
        }
    }

    void RollDice(){
        TimeDiceRoll -= Time.deltaTime;
        int diceCount1  = Random.Range(1, 6);
        switch (diceCount1){
            case 1:
                transformDice1.GetComponent<Image>().sprite = dice1;
                break;
            case 2:
                transformDice1.GetComponent<Image>().sprite = dice2;
                break;
            case 3:
                transformDice1.GetComponent<Image>().sprite = dice3;
                break;
            case 4:
                transformDice1.GetComponent<Image>().sprite = dice4;
                break;
            case 5:
                transformDice1.GetComponent<Image>().sprite = dice5;
                break;
            case 6:
                transformDice1.GetComponent<Image>().sprite = dice6;
                break;
        }
        int diceCount2  = Random.Range(1, 6);
        switch (diceCount2){
            case 1:
                transformDice2.GetComponent<Image>().sprite = dice1;
                break;
            case 2:
                transformDice2.GetComponent<Image>().sprite = dice2;
                break;
            case 3:
                transformDice2.GetComponent<Image>().sprite = dice3;
                break;
            case 4:
                transformDice2.GetComponent<Image>().sprite = dice4;
                break;
            case 5:
                transformDice2.GetComponent<Image>().sprite = dice5;
                break;
            case 6:
                transformDice2.GetComponent<Image>().sprite = dice6;
                break;
        }
        if(TimeDiceRoll < 0)
            OnDiceResult(diceCount1 + diceCount2);        
    }

    void OnDiceResult(int result){
        game.MovePlayer(result);
        InActiveDice();
    }

    public void MovePlayerResult(int CurrentPlayerId, int OldPosition, int NewPosition, bool NeedToPayRent, 
                bool CityAvailableForPurchase, bool Prison, bool IncomeTax, bool Hotel, bool RoundCompleted){
        if(NeedToPayRent){
            panelPayRent.SetActive(true);
        }else if(CityAvailableForPurchase){
            panelBuyCity.SetActive(true);
        }else if(Prison){
            panelPrison.SetActive(true);
        }else if(IncomeTax){
            panelIncomeTax.SetActive(true);
        }else if(Hotel){
            panelHotel.SetActive(true);
        }
        if(RoundCompleted){
            //round completed
        }
        
        //move player on ui
        double differenceX = 0.1 ;
        double differenceZ = 0.1 ;
        if(CurrentPlayerId < 2)
            differenceZ *= -1 ;
        if(CurrentPlayerId %3 == 0)
            differenceX *= -1 ;
        transformPlayer[CurrentPlayerId].position = 
                new Vector3((float) (ArrayCenterPositionX[NewPosition] + differenceX),
                 (float)CenterPositionY,
                 (float) (ArrayCenterPositionZ[NewPosition] + differenceZ));
    }

    public void BuyCityResult(bool result){
        panelBuyCity.SetActive(false);
        if(result){
            game.PassTurn();
        }else{
            panelInsufficientAmount.SetActive(true);
        }
    }

    public void GiveCityRentResult(bool result){
        panelPayRent.SetActive(false);
        if(result){
            game.PassTurn();
        }else{
            panelInsufficientAmount.SetActive(true);
        }
    }

    public void MortageCityResult(bool result){
        
    }

    public void DeMortageCityResult(bool result){
        if(!result){
            panelInsufficientAmount.SetActive(true);
        }
    }

    public void PayHotelFineResult(bool result){
        panelHotel.SetActive(false);
        if(result){
            game.PassTurn();
        }else{
            panelInsufficientAmount.SetActive(true);
        }
    }

    public void PayPrisonFineResult(bool result){
        panelPrison.SetActive(false);
        if(result){
            game.PassTurn();
        }else{
            panelInsufficientAmount.SetActive(true);
        }
    }

    public void PayIncomeTaxResult(bool result){
        panelIncomeTax.SetActive(false);
        if(result){
            game.PassTurn();
        }else{
            panelInsufficientAmount.SetActive(true);
        }
    }

    public void BoxTypeResult(bool CanSellOrMortage, bool CanDemortage, bool CanBuy, bool CanPayRent,
                 bool CanPayHotelFine, bool CanPayIncomeTaxFine, bool CanPayPrisonFine){
        if(CanSellOrMortage){
            //show sell or mortage dialog
        } else if(CanDemortage){
            // show demortage dialog
        } else if(CanBuy){
            //show buy dialog
        } else {
            //show city dialog
        }
    }

    public void GameEndResult(){
        //Game End Panel
    }

    public void PassTurnResult(){
        ActiveDice();
    }

    void OnQuizSolutionClicked(){

    }

    void OnBuyCloseClicked(){
        panelBuyCity.SetActive(false);
    }

    void OnGoNextClicked(){
        Debug.Log("Go Next Clicked");
        panelBuyCity.SetActive(false);
        game.PassTurn();
    }

    void OnDiceClicked(){
        Debug.Log("Dice Clicked");
        TimeDiceRoll = 1.0f;
    }

    void OnBoxClicked(){
        Debug.Log("Box Clicked");
        //Init position here
        int position = 0 ;
        game.BoxType(position);
    }

    void OnBuyClicked(){
        Debug.Log("Buy Clicked");
        if(Answered){
            game.BuyCity(CorrectAnswered);
        } else {
            // show question
        }
    }

    void OnRentClicked(){
        Debug.Log("Rent Clicked");
        if(Answered){
            game.GiveCityRent(CorrectAnswered);
        } else {
            DisplayQuestion();
        }    
    }

    void OnPrisonPayClicked(){
        game.PayPrisonFine();
    }

    void OnIncomeTaxPayClicked(){
        game.PayIncomeTax();
    }

    void OnHotelFineClicked(){
        game.PayHotelFine();
    }

    void OnGameOverClicked(){
        game.PlayerGameOver();
    }

    void OnExitClicked(){
        Debug.Log("Exit Clicked");
        panelExit.SetActive(true);
    }

    void OnExitYesClicked(){
        SceneManager.LoadScene("HomeScene");
    }

    void OnExitNoClicked(){
        panelExit.SetActive(false);
    }

    void InActiveDice(){
        buttonDice1.interactable = false;
        buttonDice2.interactable = false;
    }

    void ActiveDice(){
        buttonDice1.interactable = true;
        buttonDice2.interactable = true;
    }

    void DisplayQuestion(){
        //
    }
}
