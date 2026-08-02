using UnityEngine;

public class SpawnWinner : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;

    LevelWin levelWin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gm = GameObject.FindWithTag("Manager");
        levelWin = gm.GetComponent<LevelWin>();
        
        Spawn();
    }

    void Spawn(){
        int winner = PlayerPrefs.GetInt("winner1", 0);
        if(winner == 1){
            GameObject winnerObj = Instantiate(Player1, transform.position, transform.rotation);
        } else {
            GameObject winnerObj = Instantiate(Player2, transform.position, transform.rotation);
        }
    }
}
