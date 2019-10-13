using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PauseMenu : MonoBehaviour
{

    private GameManager gm;
    private Countdown cd;
    public static bool isPaused = false;

    public GameObject player1;
    public GameObject player2;

    public GameObject pauseMenuUi;
    public GameObject gameOverScreenUI;
    public GameObject time;

    public TextMeshProUGUI winTxt;
    public TextMeshProUGUI timeTxt;

    private bool isaTie = false;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        cd = GameObject.FindObjectOfType<Countdown>();
       
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if(cd.timeLeft <= 0.0)
        {
            if (gm.scorePlayer1 > gm.scorePlayer2)
            {
                gm.Win(player1);
                GameOver();

            }
            else if (gm.scorePlayer2 > gm.scorePlayer1)
            {
                gm.Win(player2);
                GameOver();

            }
            else
            {
                isaTie = true;
                gm.Win(player1);
                GameOver();
            }
        }
        else if (gm.scorePlayer1 == 5)
        {
            gm.Win(player1);
            GameOver();
        }
        else if (gm.scorePlayer1 == 5)
        {
            gm.Win(player2);
            GameOver();
        }

    }

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    private void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0.0f;
        isPaused = true;

    }
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 1f * 0.02f;
        time.SetActive(true);
        SceneManager.LoadScene(1);
      
    }
    private void GameOver()
    {
        time.SetActive(false);
        Time.timeScale = 0.0f;
        gameOverScreenUI.SetActive(true);




        if (isaTie)
        {
            winTxt.text = ("It's a tie");
        }
        else
        {
            winTxt.text = (gm.winner + " Wins!");
        }

        

    }
}
