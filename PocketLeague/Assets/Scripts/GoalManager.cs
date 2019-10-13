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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (this.transform.position.x > 0)
            {
                gm.Goal(gm.player1);
            }
            else
            {
                gm.Goal(gm.player2);
            }
        }

    }

}
