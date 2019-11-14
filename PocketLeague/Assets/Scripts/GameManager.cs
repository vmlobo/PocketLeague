using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject prefabBola;
    public string winner;

    private GameObject ball;


    public GameObject goalPrefab;

    private GameObject goalPlayer1;
    private GameObject goalPlayer2;
    public GameObject explosionP1;
    public GameObject explosionP2;

    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    Vector3 player1Initialpos;
    Vector3 player2Initialpos;
    Quaternion player1Initialrot;
    Quaternion player2Initialrot;

    private void Start()
    {
        // Setting volume up - Default 0.75
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat(Constants.VolumeKey, 0.75f);

        goalPlayer1 = Instantiate(goalPrefab, new Vector3(-11.33f, UnityEngine.Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);
        goalPlayer2 = Instantiate(goalPrefab, new Vector3(11.33f, UnityEngine.Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);
        ball = Instantiate(prefabBola, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
        player1Initialpos = player1.transform.position;
        player2Initialpos = player2.transform.position;
        player1Initialrot = player1.transform.rotation;
        player2Initialrot = player2.transform.rotation;
    }

    public void Goal(GameObject player)
    {
        if (player.gameObject.name == "Player1")
        {
            scorePlayer1++;
            Destroy(ball);
            GameObject expl = Instantiate(explosionP1, ball.transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 2);
            StartCoroutine(Restart());
        }
        else
        {
            scorePlayer2++;
            Destroy(ball);
            GameObject expl = Instantiate(explosionP2, ball.transform.position, Quaternion.identity) as GameObject;
            Destroy(expl, 2);
            StartCoroutine(Restart());
        }


    }
    IEnumerator Restart()
    {
 
        //Debug.Log("Pausing");

        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.3f * 0.02f;
        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;


        ball = Instantiate(prefabBola, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        Destroy(goalPlayer1);
        Destroy(goalPlayer2);
        goalPlayer1 = Instantiate(goalPrefab, new Vector3(-11f, UnityEngine.Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);
        goalPlayer2 = Instantiate(goalPrefab, new Vector3(11f, UnityEngine.Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);

        player1.transform.position = player1Initialpos;
        player2.transform.position = player2Initialpos;
        player1.transform.rotation = player1Initialrot;
        player2.transform.rotation = player2Initialrot;
        player1.GetComponent<CarController>().resetBoost();
        player2.GetComponent<CarController>().resetBoost();

        //Debug.Log("restarting");
    }


    public void Win(GameObject player)
    {
        Time.timeScale = 0f;

        if (player.name == player1.name)
        {
            winner = player1.name;

        } else if(player.name == player2.name)
        {
            winner = player2.name;
        }
      

    }

}