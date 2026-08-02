using UnityEngine;
using System.Collections.Generic;


//moves a wall when player hits a button
public class ButtonCollision : MonoBehaviour
{
    Transform wall;
    Transform start;
    Transform end;
    public float rate;
    bool wallMoved; 
    bool moveBack;

    Reset reset;

    PlayerCollision playerCollision;

    SpriteRenderer spriteRenderer;

    public Sprite unpressed;
    public Sprite pressed;

    public float moveTime = 1f;

    private List<GameObject> walls = new List<GameObject>();

    AudioManager audioManager;

    void Awake(){
        GameObject player = GameObject.FindWithTag("Player");
        playerCollision = player.GetComponent<PlayerCollision>();
        
        GameObject manager = GameObject.FindWithTag("Manager");
        reset = manager.GetComponent<Reset>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        wall = transform.GetChild(0);
        start = transform.GetChild(1);
        end = transform.GetChild(2);

        spriteRenderer.sprite = unpressed;

        GameObject soundObject = GameObject.FindWithTag("audio");
       if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
       }

    }
    
    void OnCollisionEnter2D(Collision2D other){
        
        if(other.gameObject.tag == "Player"){
            if(wallMoved){
                moveBack = true;
                wallMoved = false;
                spriteRenderer.sprite = unpressed;
            }else{
            wallMoved = true;
            moveBack = false;
            walls.Add(wall.gameObject);
            spriteRenderer.sprite = pressed;
            }
            audioManager.Play("click");
        }
    }
    void Update(){
        if(wallMoved && rate <= 1f){
            rate += Time.deltaTime/moveTime;
            wall.position = Vector3.Lerp(start.position,end.position,rate);

        }
        else if(moveBack && rate >= 0f){
            rate -= Time.deltaTime/moveTime;
            wall.position = Vector3.Lerp(start.position,end.position,rate);
        }
    }
    public void ResetWall(){
         foreach(GameObject wall in walls){
        wall.transform.position = Vector3.Lerp(start.position,end.position,0);
                wallMoved = false;
                rate = 0f;
         }
         spriteRenderer.sprite = unpressed;
    }

}

