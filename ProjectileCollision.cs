using Unity.VisualScripting;
using UnityEngine;

public class ProjectileCollision : MonoBehaviour
{

    Transform circleRay;




void Awake(){
    circleRay = transform.GetChild(0);
}

    void FixedUpdate(){
        int groundLayerMask = 1<<6;
        int wallLayerMask = 1 << 7;

        //create raycasts 
        RaycastHit2D rightHit = Physics2D.Raycast(circleRay.position,
        transform.TransformDirection(Vector2.right),.5f, wallLayerMask);

        RaycastHit2D rightGroundHit = Physics2D.Raycast(circleRay.position,
        transform.TransformDirection(Vector2.right),.5f, wallLayerMask);

        RaycastHit2D leftHit = Physics2D.Raycast(circleRay.position,
        transform.TransformDirection(Vector2.left), .5f, groundLayerMask);

        RaycastHit2D leftGroundHit = Physics2D.Raycast(circleRay.position,
        transform.TransformDirection(Vector2.left), .5f, groundLayerMask);

        if(leftHit.collider !=null || rightHit.collider != null || leftGroundHit.collider !=null || rightGroundHit.collider != null){
            Destroy(this.gameObject);
        }
    }
    
}
