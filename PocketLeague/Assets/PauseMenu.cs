using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{

    private GameManager gm;
    public static bool isPaused = false;

    public GameObject player1;
    public GameObject player2;

    public GameObject pauseMenuUi;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
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

        if(gm.scorePlayer1 > gm.scorePlayer2 && gm.scorePlayer1 > 5)
        {
            gm.Win(player1);
        }
        else if (gm.scorePlayer1 < gm.scorePlayer2 && gm.scorePlayer1 > 5)
        {
            gm.Win(player2);
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
}
