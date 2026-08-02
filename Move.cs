using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Move : MonoBehaviour
{
    public float speed;
    public float height;
    Rigidbody2D square_rbody;
    Transform groundRay, groundRay2, rightRay, rightRay2, leftRay, leftRay2;
    public bool isGrounded;

    public Animator animator;
    public float facingX;

    bool jumping;

    AudioManager audioManager;
    void Awake()
    {
        //get object body
        square_rbody = GetComponent<Rigidbody2D>();

        //get raycasts
        groundRay = transform.GetChild(0);
        rightRay = transform.GetChild(1);
        leftRay = transform.GetChild(2);
        groundRay2 = transform.GetChild(3);
        rightRay2 = transform.GetChild(4);
        leftRay2 = transform.GetChild(5);

        animator = GetComponent<Animator>();

        GameObject soundObject = GameObject.FindWithTag("audio");
       if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
       }

    }

    void Update(){

        if(Input.GetKey(KeyCode.D)){
            facingX = 1;
            animator.SetBool("isWalking", true);
        }
        if(Input.GetKey(KeyCode.A)){
            facingX = -1;
            animator.SetBool("isWalking", true);
        } 
        if(Input.GetKeyDown(KeyCode.W)){
            animator.SetBool("isJumping",true);
            jumping = true;
        }else{
            animator.SetBool("isJumping", false);
        }
        Walk();
        //stop sliding after done moving
        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)){
            square_rbody.linearVelocityX = 0f;
            animator.SetBool("isWalking", false);
        }
       
    }

    void FixedUpdate(){
        //set layers to variables
        int groundLayerMask = 1 << 6;
        int wallLayerMask = 1 << 7;

        //create raycasts 
        RaycastHit2D hit = Physics2D.Raycast(groundRay.position,
        transform.TransformDirection(Vector2.down), 0.03f, groundLayerMask);

        RaycastHit2D hit2 = Physics2D.Raycast(groundRay2.position,
        transform.TransformDirection(Vector2.down), 0.03f, groundLayerMask);

        RaycastHit2D rightWalled = Physics2D.Raycast(rightRay.position,
        transform.TransformDirection(Vector2.right), 0.03f, 
        wallLayerMask);

        RaycastHit2D rightWalled2 = Physics2D.Raycast(rightRay2.position,
        transform.TransformDirection(Vector2.right), 0.03f, 
        wallLayerMask);

        RaycastHit2D leftWalled = Physics2D.Raycast(leftRay.position,
        transform.TransformDirection(Vector2.left), 0.03f, 
        wallLayerMask);

        RaycastHit2D leftWalled2 = Physics2D.Raycast(leftRay2.position,
        transform.TransformDirection(Vector2.left), 0.03f, 
        wallLayerMask);

        RaycastHit2D rightGroundWalled = Physics2D.Raycast(rightRay.position,
        transform.TransformDirection(Vector2.right), 0.03f, 
        groundLayerMask);

        RaycastHit2D rightGroundWalled2 = Physics2D.Raycast(rightRay2.position,
        transform.TransformDirection(Vector2.right), 0.03f, 
        groundLayerMask);

        RaycastHit2D leftGroundWalled = Physics2D.Raycast(leftRay.position,
        transform.TransformDirection(Vector2.left), 0.03f, 
        groundLayerMask);

        RaycastHit2D leftGroundWalled2 = Physics2D.Raycast(leftRay2.position,
        transform.TransformDirection(Vector2.left), 0.03f, 
        groundLayerMask);

        //check for jump
        if(hit.collider != null || hit2.collider != null){
            isGrounded = true;
        }else{
            isGrounded = false;
        }

        //jump
        if(jumping && isGrounded){
            square_rbody.linearVelocity = new Vector2(square_rbody.linearVelocityX, height);
            isGrounded = false;
            jumping = false;
            
            audioManager.Play("jump");
        }

        //move, if statement is for sticking to walls
        if (Input.GetKey(KeyCode.D)){
            if(rightWalled.collider == null && rightGroundWalled.collider == null && 
            rightWalled2.collider == null && rightGroundWalled2.collider == null){
            square_rbody.linearVelocityX = (1f * speed);
            }
        }
        else if(Input.GetKey(KeyCode.A)){
            if(leftWalled.collider == null && leftGroundWalled.collider == null &&
            leftWalled2.collider == null && leftGroundWalled2.collider == null){
            square_rbody.linearVelocityX = (-1f * speed);
            }
        }

    }

    void Walk(){
        animator.SetFloat("Xinput",facingX);
    }
}
