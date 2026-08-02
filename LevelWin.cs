using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelWin : MonoBehaviour
{

    public bool Player1Win = false;
    public bool Player2Win = false;

    public bool winner1 = false;
    GameManager gameManager;

    AudioManager audioManager;

   void Awake(){
    gameManager = this.GetComponent<GameManager>();
    GameObject soundObject = GameObject.FindWithTag("audio");
    if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
    }
   }
   //Change to the next scene if you win
    public void checkWin(){
        string curr = SceneManager.GetActiveScene().name;
        bool winCondition = false;

        if(curr == "Level8"){
            winCondition = Player1Win || Player2Win;
            if(Player1Win){
                PlayerPrefs.SetInt("winner1", 1);
            }else if(Player2Win){
                PlayerPrefs.SetInt("winner1", 0);
            }
        }else{
            winCondition = Player1Win && Player2Win;
        }

        if(winCondition){
            
            audioManager.Play("win");

            gameManager.AddScore();
            gameManager.SaveStats();
            SceneManage.instance.LoadNextScene();
        }
    }
}
