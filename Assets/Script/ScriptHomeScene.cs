using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptHomeScene : MonoBehaviour{

    public static string[] namePlayer;
    public static int playerCount;

    public GameObject panelNewGame;
    public GameObject panelExit;
    public Button buttonPlayer2;
    public Button buttonPlayer3;
    public Button buttonPlayer4;
    public Button buttonNewGameClose;
    public Button buttonStart;
    public Button buttonPlay;
    public Button buttonHowToPlay;
    public Button buttonExitYes;
    public Button buttonExitNo;
    public InputField inputField1;
    public InputField inputField2;
    public InputField inputField3;
    public InputField inputField4;

    void Awake(){
        namePlayer = new string[4];
    }

    void Start(){
        panelExit.SetActive(false);
        panelNewGame.SetActive(false);
        buttonHowToPlay.onClick.AddListener(OnHowToPlayClick);
        buttonPlay.onClick.AddListener(OnPlayClick);
        buttonExitNo.onClick.AddListener(OnExitNoClicked);
        buttonExitYes.onClick.AddListener(OnExitYesClicked);
        buttonPlayer2.onClick.AddListener(OnTwoPlayerCountClicked);
        buttonPlayer3.onClick.AddListener(OnThreePlayerCountClicked);
        buttonPlayer4.onClick.AddListener(OnFourPlayerCountClicked);
        buttonStart.onClick.AddListener(OnNewStartClicked);
        buttonNewGameClose.onClick.AddListener(OnNewStartCloseClicked);
        inputField1.interactable = true;
        inputField2.interactable = true;
        inputField3.interactable = false;
        inputField4.interactable = false;
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            if(panelNewGame.active)
                OnNewStartCloseClicked();
            else
                OnExitClicked();
        }
    }

    public void OnPlayClick(){
        if (panelNewGame != null){
            panelNewGame.SetActive(true);
        }
    }

    public void OnHowToPlayClick(){
        //??
    }

    public void OnTwoPlayerCountClicked(){
        inputField1.interactable=true;
        inputField2.interactable=true;
        inputField3.interactable=false;
        inputField4.interactable=false;
        playerCount = 2 ;
    }

    public void OnThreePlayerCountClicked(){
        inputField1.interactable=true;
        inputField2.interactable=true;
        inputField3.interactable=true;
        inputField4.interactable=false;
        playerCount = 3 ;
    }
    
    public void OnFourPlayerCountClicked(){
        inputField1.interactable=true;
        inputField2.interactable=true;
        inputField3.interactable=true;
        inputField4.interactable=true;
        playerCount = 4 ;
    }

    public void OnNewStartClicked(){
        if (inputField1.interactable && inputField1.text.Length==0){
            inputField1.text = "Player 1";
        }
        if (inputField2.interactable && inputField2.text.Length==0){
            inputField2.text = "Player 2";
        }
        if (inputField3.interactable && inputField3.text.Length==0){
            inputField3.text = "Player 3";
        }
        if (inputField4.interactable && inputField4.text.Length==0){
            inputField4.text = "Player 4";
        }
        if (inputField1.interactable)
            namePlayer[0] = inputField1.text ;
        if (inputField2.interactable)
            namePlayer[1] = inputField2.text ;
        if (inputField3.interactable)
            namePlayer[2] = inputField3.text ;
        if (inputField4.interactable)
            namePlayer[4] = inputField4.text ;
        StartGameScene();
    }

    public void OnNewStartCloseClicked(){
        if(panelExit != null){
            panelNewGame.SetActive(false);
        }
    }

    public void OnExitClicked(){
        panelExit.SetActive(true);
    }

    public void OnExitYesClicked(){
        panelExit.SetActive(false); 
        panelNewGame.SetActive(false);
        Application.Quit();
    }

    public void OnExitNoClicked(){
        if(panelExit != null){
            panelExit.SetActive(false);
        }
    }

    void StartGameScene(){
        SceneManager.LoadScene("GameScene");
    }
}
