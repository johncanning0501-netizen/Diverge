using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform start;
    public Transform end;
    private Transform temp;

    public float rotation;

    float rate;

    public float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        rate += timer * Time.deltaTime;
        transform.position = Vector3.Lerp(start.position,end.position,rate);
        transform.Rotate(0f, 0f, rotation * Time.deltaTime);
        if(rate >= 1f){
            temp = start;
            start = end;
            end = temp;
            rate = 0f;
        }
    }
}
