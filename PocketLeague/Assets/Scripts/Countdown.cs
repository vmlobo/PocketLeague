using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using TMPro;
public class Countdown : MonoBehaviour
{
    public float gameTimeLeft = 60; //Seconds Overall
    public TextMeshProUGUI gameCountdown; //UI Text Object

    // Countdown to starting the game
    public float startGamecountDownTimer = 3; //Seconds Overall
    public TextMeshProUGUI startGameCountdown; //UI Text Object

    void Start()
    {
        Time.timeScale = 1; //Just making sure that the timeScale is right
    }
    void Update()
    {
        if (MultiplayerGameManager.hasGameStarted)
        {
            gameTimeLeft -= Time.deltaTime;
            gameCountdown.text = ((int)gameTimeLeft).ToString(); //Showing the Score on the Canvas
        } else if (shouldCountDown)
        {
            startGamecountDownTimer -= Time.deltaTime;
            startGameCountdown.text = ((int)startGamecountDownTimer).ToString();
            if (startGamecountDownTimer < 0)
            {
                MultiplayerGameManager.StartGame();
                shouldCountDown = false;
                startGameCountdown.text = "";
            }

        }
    }

    private bool shouldCountDown = false;
    public void initiateCountdown()
    {
        Debug.Log("Countdown initiating");
        shouldCountDown = true;
    }
}
