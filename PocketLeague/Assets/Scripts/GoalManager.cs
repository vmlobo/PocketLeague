using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject prefabBola;
    public GameObject ball;

    //public Rigidbody rb;
    //private CarController cr;

    int scorePlayer1 = 0;
    int scorePlayer2 = 0;

    Vector3 player1pos;
    Vector3 player2pos;
    Vector3 ballpos;


    private void Start()
    {

        ball = Instantiate(prefabBola, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
        player1pos = player1.transform.position;
        player2pos = player2.transform.position;
        ballpos = ball.transform.position;
        //CarController cc = GetComponent(typeof(CarController)) as CarController;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "RightPost")
        {
            Goal(player1);

        }
        else if (other.gameObject.name == "LeftPost")
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
        //cc.enable = false;
        yield return new WaitForSeconds(3);
        ball = Instantiate(prefabBola, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);



        //cc.enable = true



        Debug.Log("restarting");
    }
}
