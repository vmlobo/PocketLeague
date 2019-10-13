using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float impulseForce;
    public float jumpForce;
    public float boostForce;
    public float turnRate;
    //public float isGroundedRange;

    private bool isGrounded;
    private Rigidbody body;
    private Rigidbody rlW;
    private Rigidbody rrW;
    //private Rigidbody flW;
    //private Rigidbody frW;
    private float jumpCD;
    private int jumpCount;
    private ParticleSystem ps;
    private float boost;

    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody>();
        rlW = transform.GetChild(4).GetComponent<Rigidbody>();
        rrW = transform.GetChild(5).GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();

        boost = 100;
        jumpCount = 0;
        jumpCD = 5; //TODO cd

    }

    // Update is called once per frame
    void Update()
    {
        isGroundedCheck();

        if (Input.GetKey(KeyCode.D)) //TODO handle input for different players
        {
            if (isGrounded)
                body.AddForce(transform.forward * impulseForce, ForceMode.Acceleration); //TODO review force mode
            else
                body.AddTorque(transform.right*turnRate, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (isGrounded)
                body.AddForce(-transform.forward * impulseForce, ForceMode.Acceleration); //TODO review force mode
            else
                body.AddTorque(-transform.right*turnRate, ForceMode.Acceleration);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump();
        }
        if (Input.GetKey(KeyCode.LeftShift) && boost > 0)
        {
            Debug.Log("started boost");
            boost -= 1.0f;
            ps.Play();
            body.AddForce(transform.forward * boostForce, ForceMode.Acceleration);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || boost <= 0)
        {
            Debug.Log("stopped boost");
            ps.Stop();

        }

    }

    private void jump() //TODO tudo
    {

        body.AddForce(transform.up * jumpForce, ForceMode.Impulse);

    }

    void isGroundedCheck()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, 0.45f);
        //Debug.DrawRay(transform.position, -transform.up*isGroundedRange);
        //Debug.Log("isGrounded:" + isGrounded);
    } //TODO while !isGrounded trail renderer



}
