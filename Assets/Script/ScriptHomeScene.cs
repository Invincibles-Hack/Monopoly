using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScriptHomeScene : MonoBehaviour{

    void Start(){
        Button buttonPlay = transform.Find("ButtonPlay").GetComponent<Button>();
        Button buttonHowToPlay = transform.Find("ButtonHowToPlay").GetComponent<Button>();
        buttonPlay.onClick.AddListener(OnPlayClick);
        buttonHowToPlay.onClick.AddListener(OnHowToPlayClick);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            OnExitClicked();
        }
    }

    void OnPlayClick(){
        //Show New Game Dialog        
    }

    void OnHowToPlayClick(){
        //??
    }

    void OnTwoPlayerCountClicked(){
        
    }

    void OnThreePlayerCountClicked(){
        
    }
    
    void OnFourPlayerCountClicked(){
        
    }

    void OnNewStartClicked(){
        //Validate Name
    }

    void OnNewStartCloseClicked(){

    }

    void OnExitClicked(){
        //Show Exit Dialog
    }

    void OnExitYesClicked(){

    }

    void OnExitNoClicked(){

    }

    void StartGameScene(){
        SceneManager.LoadScene("GameScene");
    }
}
