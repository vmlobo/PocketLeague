﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO adjust ball params
//TODO post processing

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

    public int scorePlayer1 = 0;
    public int scorePlayer2 = 0;

    Vector3 player1Initialpos;
    Vector3 player2Initialpos;
    Quaternion player1Initialrot;
    Quaternion player2Initialrot;
    Vector3 ballpos;


    private void Start()
    {
        goalPlayer1 = Instantiate(goalPrefab, new Vector3(-11.33f, Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);
        goalPlayer2 = Instantiate(goalPrefab, new Vector3(11.33f, Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);
        ball = Instantiate(prefabBola, new Vector3(0.0f, 5.0f, 0.0f), Quaternion.identity);
        player1Initialpos = player1.transform.position;
        player2Initialpos = player2.transform.position;
        player1Initialrot = player1.transform.rotation;
        player2Initialrot = player2.transform.rotation;
         ballpos = ball.transform.position;

    }

    public void Goal(GameObject player)
    {
        if (player.gameObject.name == "Player1")
        {
            scorePlayer1++;

            Debug.Log(scorePlayer1); 
            Debug.Log(scorePlayer2);

            StartCoroutine(Restart());
        }
        else
        {
            scorePlayer2++;
            
            Debug.Log(scorePlayer1);
            Debug.Log(scorePlayer2);

            StartCoroutine(Restart());
        }


    }
    IEnumerator Restart()
    {
 
        Debug.Log("Pausing");


        Destroy(ball);//TODO explosao bola
        
        Time.timeScale = 0.5f;
        Time.fixedDeltaTime = 0.3f * 0.02f;
        yield return new WaitForSeconds(2f);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 1f * 0.02f;


        ball = Instantiate(prefabBola, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);

        Destroy(goalPlayer1);
        Destroy(goalPlayer2);
        goalPlayer1 = Instantiate(goalPrefab, new Vector3(-11f, Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);
        goalPlayer2 = Instantiate(goalPrefab, new Vector3(11f, Random.Range(-3.38f, 4.20f), 0.0f), Quaternion.identity);

        player1.transform.position = player1Initialpos;
        player2.transform.position = player2Initialpos;
        player1.transform.rotation = player1Initialrot;
        player2.transform.rotation = player2Initialrot;
        player1.GetComponent<CarController>().boost = 100;
        player2.GetComponent<CarController>().boost = 100;

        Debug.Log("restarting");
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