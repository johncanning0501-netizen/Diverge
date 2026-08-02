using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{

    public Rigidbody2D projectile;
    float spawnTimer;
    public float spawnSpeed;
    public float lifeTime;
    public float speed;
    public bool isLeft;
    public RuntimeAnimatorController left;
    public RuntimeAnimatorController right;
    public RuntimeAnimatorController leftCanon;
    public RuntimeAnimatorController rightCanon;

    Animator canonAnim;
    AudioManager audioManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake(){
        canonAnim = GetComponent<Animator>();

        GameObject soundObject = GameObject.FindWithTag("audio");
        if(soundObject != null){
       audioManager = soundObject.GetComponent<AudioManager>();
       }
    }
   

    // Update is called once per frame
    void Update()
    {
        //adds time to a timer
        spawnTimer += Time.deltaTime;

        //if the timer is larger then whatever spawn speed is set to then spawn and reset timer
        if(spawnTimer >= spawnSpeed){
            SpawnProjectile();
            spawnTimer = 0f;
        }

        if(isLeft){
            canonAnim.runtimeAnimatorController = leftCanon;
        }else{
            canonAnim.runtimeAnimatorController = rightCanon;
        }
        
    }
    void SpawnProjectile(){

        //direction it spawns
        Vector3 direction = new Vector3(0,0,0);

        if(isLeft){
            direction = new Vector3(-1,0,0);
        }else{
            direction = new Vector3(1,0,0);
        }

        Rigidbody2D clone = Instantiate(projectile, transform.position + direction, transform.rotation);

        if(audioManager != null){
        audioManager.Play("fireball");
        }
        
        Animator animator = clone.GetComponent<Animator>();
            if(isLeft){
                clone.linearVelocityX = -1f * speed;
                if(animator != null){
                animator.runtimeAnimatorController = left;
                }
            }
            else{
                clone.linearVelocityX = 1f * speed;
                if(animator!=null){
                animator.runtimeAnimatorController = right;
                }
            }
            //Destroys the object based on whatever lifetime is set to
        Destroy(clone.gameObject, lifeTime);
    }
    
}
