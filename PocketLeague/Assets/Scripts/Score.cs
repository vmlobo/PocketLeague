using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI score;
    private GameManager gm;

    public static TextMeshProUGUI player1Text;
    public static TextMeshProUGUI player2Text;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        player1Text = GameObject.Find("Player1Text").GetComponent<TextMeshProUGUI>();
        player2Text = GameObject.Find("Player2Text").GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    void Update()
    {
        if (MultiplayerGameManager.hasGameStarted)
            score.text = gm.scorePlayer1.ToString() + " - " + gm.scorePlayer2.ToString();
    }

    public static void setPlayersNickNames(string opponentName, bool isPlayer1)
    {
        player1Text.text = isPlayer1 ? PlayerPrefs.GetString(Constants.NicknameKey, "Im Player 1") : opponentName;
        player2Text.text = isPlayer1 ? opponentName : PlayerPrefs.GetString(Constants.NicknameKey, "Im Player 2");
    }
}
