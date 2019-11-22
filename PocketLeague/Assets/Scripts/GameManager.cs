using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    }

    public void Goal(string player)
    {
        if (player == "Player1")
        {
            scorePlayer1++;
            GameObject expl = Instantiate(explosionP1, GameSetupController.ball.transform.position, Quaternion.identity) as GameObject;
            Destroy(GameSetupController.ball);
            Destroy(expl, 2);
            StartCoroutine(Restart());
        }
        else
        {
            scorePlayer2++;
            GameObject expl = Instantiate(explosionP2, GameSetupController.ball.transform.position, Quaternion.identity) as GameObject;
            Destroy(GameSetupController.ball);
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

        GameSetupController.instatiateObjects();
    }


    public void Win(string player)
    {
        Time.timeScale = 0f;
        winner = player;
        //if (player.name == player1.name)
        //{
        //    winner = player1.name;

        //} else if(player.name == player2.name)
        //{
        //    winner = player2.name;
        //}
    }



}