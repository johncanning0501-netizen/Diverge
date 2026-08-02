using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public static SceneManage instance;

    public enum Scene{
        SampleScene,
        MainMenu,
        Level1,
        Level2,
        Level3,
        Level4,
        Level5,
        Level6,
        Level7,
        Level8,
        LevelEnd
    }
  
    void Awake()
    {
        if(instance == null){
        instance = this;
        DontDestroyOnLoad(gameObject);
        }else if (instance != this){
            DontDestroyOnLoad(gameObject);
        }
    }

    void Update()
    {
       
    }

    public void isPressed(){
        Debug.Log("The button was pressed");
    }

    public void LoadScene(Scene scene){
        SceneManager.LoadScene(scene.ToString());
    }
    public void LoadNewGame(){
        //reset player prefs when starting a new game
        PlayerPrefs.SetInt("score", 0);
        PlayerPrefs.SetInt("deathCount",0);
        SceneManager.LoadScene(Scene.Level1.ToString());
    }
    public void LoadMainMenu(){
        SceneManager.LoadScene(Scene.MainMenu.ToString());
    }
    public void LoadNextScene(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
