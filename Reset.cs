using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class Reset : MonoBehaviour
{
    public PlayerCollision player1;
    public PlayerCollision player2;
    public ButtonCollision[] buttonCollision;

    GameManager gm;

    AudioManager audioManager;



    void Awake(){

       GameObject manager = this.gameObject;
       gm = manager.GetComponent<GameManager>();

       GameObject soundObject = GameObject.FindWithTag("audio");
       if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
       }
       
    }

    public void OnPlayerDeath(){
        player1.ResetPlayer();
        player2.ResetPlayer();
        if(buttonCollision.Length > 0){
        for(int i = 0; i < buttonCollision.Length; i++){
            buttonCollision[i].ResetWall();
        }
        
        }
        gm.coinCount = 0;
        gm.deathCount++;

        audioManager.Play("death");
    }

    void Update(){
        //resets the players position manually
        if(Input.GetKeyUp(KeyCode.R)){
            player1.ResetPlayer();
            player2.ResetPlayer();
            if(buttonCollision != null){
            for(int i = 0; i < buttonCollision.Length; i++){
            buttonCollision[i].ResetWall();
        }
            }
            gm.coinCount = 0;
            gm.deathCount++;
        }
    }
}
