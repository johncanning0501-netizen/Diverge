using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
/*
*
*/
public class PlayerCollision : MonoBehaviour
{

    Reset reset;

    public bool isDead;
    public int coinAmount;
    public Transform start;
    Rigidbody2D rb; 
    private List<GameObject> collectedCoins = new List<GameObject>();

    LevelWin levelWin;
    public bool isPlayer1;

    public int deathCount;

    AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        isDead = false;
        GameObject manager = GameObject.FindWithTag("Manager");
        reset = manager.GetComponent<Reset>();
        levelWin = manager.GetComponent<LevelWin>();

        GameObject soundObject = GameObject.FindWithTag("audio");
       
       if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
       }

        
    }

    //This handles the game over as well as collecting coins by detecting
    //what type of object is touching the player.
    void OnCollisionEnter2D(Collision2D other){

        if(other.gameObject.tag == "obstacle" ){
            isDead = true;
            reset.OnPlayerDeath();
            

            
        }else if(other.gameObject.tag == "coin"){
            coinAmount++;
            collectedCoins.Add(other.gameObject);
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            other.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            audioManager.Play("pickupCoin");
        }
    }
    void OnCollisionExit2D(Collision2D other){
       
       
    }

    //This handles the win condition and resets the player and coins
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "winzone"){
            if(isPlayer1){
                levelWin.Player1Win = true;
            }
            else{
                levelWin.Player2Win = true;
            }
            levelWin.checkWin();
        
        }
            
    }
    
    void OnTriggerExit2D(Collider2D other){
        if(isPlayer1){
                levelWin.Player1Win = false;
            }
            else{
                levelWin.Player2Win = false;
            }
    }

    void FixedUpdate()
    {
        if(isDead){
            isDead = false;
        }
        
    }
    
    public void ResetPlayer(){
        transform.position = start.position;
        rb.linearVelocityX = (0f);

        foreach(GameObject coin in collectedCoins){
            coin.GetComponent<SpriteRenderer>().enabled = true;
            coin.GetComponent<BoxCollider2D>().enabled = true;
        }
            collectedCoins.Clear();
            coinAmount = 0;
            deathCount++;
    }
}

