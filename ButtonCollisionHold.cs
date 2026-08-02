using UnityEngine;
using System.Collections.Generic;

public class ButtonCollisionHold : MonoBehaviour
{
    Transform wall;
    Transform start;
    Transform end;
    public float rate;

    bool pressing;

    Reset reset;

    SpriteRenderer spriteRenderer;

    public Sprite unpressed;
    public Sprite pressed;

    public float moveTime = 1f;

    PlayerCollision playerCollision;

    private List<GameObject> walls = new List<GameObject>();

    AudioManager audioManager;

    void Awake(){
        GameObject player = GameObject.FindWithTag("Player");
        playerCollision = player.GetComponent<PlayerCollision>();

        GameObject manager = GameObject.FindWithTag("Manager");
        reset = manager.GetComponent<Reset>();

        wall = transform.GetChild(0);
        start = transform.GetChild(1);
        end = transform.GetChild(2);

        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = unpressed;

        GameObject soundObject = GameObject.FindWithTag("audio");
        if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
       }

    }
    
    void OnCollisionStay2D(Collision2D other){
        
        if(other.gameObject.tag == "Player"){
            pressing = true;
            spriteRenderer.sprite = pressed;
        }
    }
    void OnCollisionEnter2D(Collision2D other){
        audioManager.Play("click");
    }

    void OnCollisionExit2D(Collision2D other){
        pressing = false;
        spriteRenderer.sprite = unpressed;
        audioManager.Play("click");
    }
    
    void Update(){
        if(pressing && rate <= 1f){
            rate += Time.deltaTime/moveTime;
            wall.position = Vector3.Lerp(start.position,end.position,rate);

        }
        else if(pressing == false && rate > 0f){
            rate -= Time.deltaTime/moveTime;
            wall.position = Vector3.Lerp(start.position, end.position, rate);
        }
    }
}
