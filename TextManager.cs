using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{

    TextMeshProUGUI textMesh;
    GameManager gameManager;
    LevelWin levelWin;
    string winner;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject manager = GameObject.FindWithTag("Manager");
        gameManager= manager.GetComponent<GameManager>();
        levelWin = manager.GetComponent<LevelWin>();

        textMesh = this.GetComponent<TextMeshProUGUI>();

        int prefs = PlayerPrefs.GetInt("winner1");
        if(prefs == 1){
            winner = "Red";
        }else{
            winner = "Green";
        }
    }

    // Update is called once per frame
    void Update()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if(sceneName == "MainMenu"){
            textMesh.enabled = false;
            PlayerPrefs.SetInt("score",0);
        }
        if(sceneName == "LevelEnd"){
            textMesh.text = " YOU WIN!!\n Score: " + PlayerPrefs.GetInt("score") + "\n-Deaths: " + 
            gameManager.deathCount + "\n-------------" + "\n Total Score: " + (PlayerPrefs.GetInt("score")-(gameManager.deathCount*10))
            + "\n Winner: " + winner;
        }
        else{
        textMesh.enabled = true;
        textMesh.text = "Score: " + PlayerPrefs.GetInt("score") + "       Deaths: " + 
            gameManager.deathCount + "      Coins: " + gameManager.coinCount;
        }
    }
}
