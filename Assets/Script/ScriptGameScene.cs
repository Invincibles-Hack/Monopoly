using UnityEngine;
using UnityEngine.UI;

public class ScriptGameScene : MonoBehaviour{

    public Sprite dice1, dice2, dice3, dice4, dice5, dice6;

    private float timeDiceRoll;
    private Transform transformDice;

    void Start(){
        transformDice = transform.Find("ButtonDice");
        transformDice.GetComponent<Button>().onClick.AddListener(OnDiceClick);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Escape)){
            ShowExitDialog();
        }
        if(timeDiceRoll > 0.0f){
            RollDice();
        }
    }

    void OnDiceClick(){
        timeDiceRoll = 1.0f;
    }

    void OnDiceResult(int result){
        Debug.Log(result);
    }

    void RollDice(){
        timeDiceRoll -= Time.deltaTime;
        int diceCount  = Random.Range(1, 6);
        switch (diceCount){
            case 1:
                transformDice.GetComponent<Image>().sprite = dice1;
                break;
            case 2:
                transformDice.GetComponent<Image>().sprite = dice2;
                break;
            case 3:
                transformDice.GetComponent<Image>().sprite = dice3;
                break;
            case 4:
                transformDice.GetComponent<Image>().sprite = dice4;
                break;
            case 5:
                transformDice.GetComponent<Image>().sprite = dice5;
                break;
            case 6:
                transformDice.GetComponent<Image>().sprite = dice6;
                break;
        }
        if(timeDiceRoll < 0.0f){
            OnDiceResult(diceCount);
        }
    }

    void ShowExitDialog(){
        Debug.Log("Exit Clicked");
    }
}
