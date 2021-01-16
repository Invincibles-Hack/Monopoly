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
            ShowExitDialog();
        }
    }

    void OnPlayClick(){
        Debug.Log("Play Clicked");
        StartGameScene();        
    }

    void OnHowToPlayClick(){
        Debug.Log("How To Play Clicked");
    }

    void ShowExitDialog(){
        Debug.Log("Exit Clicked");
    }

    void StartGameScene(){
        SceneManager.LoadScene("GameScene");
    }
}
