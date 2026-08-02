using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    public PlayerCollision player1;
    public PlayerCollision player2;
    Reset reset;
    LevelWin levelWin;

    //float timer = 30;
    public int coinCount;
    public int deathCount;

    public int score;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        GameObject manager = this.gameObject;
        reset = manager.GetComponent<Reset>();
        levelWin = manager.GetComponent<LevelWin>();

        deathCount = PlayerPrefs.GetInt("deathCount");
        score = PlayerPrefs.GetInt("score");

        coinCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if(player1 != null){
        coinCount = player1.coinAmount + player2.coinAmount;
        }


    if(player1 !=null){
        if(player1.isDead || player2.isDead){
            coinCount = 0;
        }
    }

        // timer -= Time.deltaTime;
        // if(timer <= 0){
        //     player1.ResetPlayer();
        //     player2.ResetPlayer();
        //     buttonCollision.ResetWall();
        //     timer = 30f;
        // }
    }

    public void AddScore(){
        score += coinCount * 100;
    }
    public void SaveStats(){
        PlayerPrefs.SetInt("deathCount", deathCount);
        PlayerPrefs.SetInt("score", score);
    }
}
