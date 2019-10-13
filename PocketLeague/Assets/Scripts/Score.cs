using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    public TextMeshProUGUI score;
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
 

    }
    // Update is called once per frame
    void Update()
    {
        score.text = gm.scorePlayer1.ToString() + " - " + gm.scorePlayer2.ToString();
     
    }
}
