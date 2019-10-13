using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject prefabBola;
    private GameObject ball;

    public GameObject goalPrefab;
    private GameObject goalPlayer1;
    private GameObject goalPlayer2;


    //public Rigidbody rb;
    //private CarController cr;

    int scorePlayer1 = 0;
    int scorePlayer2 = 0;

    Vector3 player1pos;
    Vector3 player2pos;
    Vector3 ballpos;


    private void Start()
    {
        goalPlayer1 = Instantiate(goalPrefab, new Vector3(-12.0f, Random.Range(-11.87f, 8.57f), 0.0f), Quaternion.identity);
        goalPlayer2 = Instantiate(goalPrefab, new Vector3(18.9f, Random.Range(-11.87f, 8.57f), 0.0f), Quaternion.identity);
        ball = Instantiate(prefabBola, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        player1pos = player1.transform.position;
        player2pos = player2.transform.position;
        ballpos = ball.transform.position;
        //CarController cc = GetComponent(typeof(CarController)) as CarController;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == goalPlayer2)
        {
            Goal(player1);

        }
        else if (collision.gameObject == goalPlayer1)
        {
            Goal(player2);
        }
    }

    private void Goal(GameObject player)
    {
        
        if (player.gameObject.name == "Player1")
        {
            scorePlayer1++;

            Debug.Log(scorePlayer1);
            Debug.Log(scorePlayer2);

            player1.transform.position = player1pos;
            player2.transform.position = player2pos;
            ball.transform.position = ballpos;
         

            StartCoroutine(Restart());


        }
        else
        {
            scorePlayer2++;
            
            Debug.Log(scorePlayer1);
            Debug.Log(scorePlayer2);

            player1.transform.position = player1pos;
            player2.transform.position = player2pos;
            ball.transform.position = ballpos;


            StartCoroutine(Restart());
        }


    }
    IEnumerator Restart()
    {
 
        Debug.Log("Pausing");


        Destroy(ball);
        Destroy(goalPlayer1);
        Destroy(goalPlayer2);
        //cc.enable = false;
        yield return new WaitForSeconds(3);
        ball = Instantiate(prefabBola, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        goalPlayer1 = Instantiate(goalPrefab, new Vector3(-12.0f, Random.Range(-11.87f, 8.57f), 85), Quaternion.identity);
        goalPlayer2 = Instantiate(goalPrefab, new Vector3(18.9f, Random.Range(-11.87f, 8.57f), 85), Quaternion.identity);



        //cc.enable = true



        Debug.Log("restarting");
    }
}
