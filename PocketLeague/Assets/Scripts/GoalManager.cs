using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{

    public GameManager gm;

    private void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    // Eventually, allow users to give nicknames
    //
    //
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (gameObject.tag == "Goal1")
            {
                gm.Goal("Player 1");
            }
            else if (gameObject.tag == "Goal2")
            {
                gm.Goal("Player 2");
            }
        }

    }

}
