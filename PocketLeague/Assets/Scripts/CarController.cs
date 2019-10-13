﻿using System;
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

    }

    // Update is called once per frame
    void Update()
    {
        isGroundedCheck();

        if (transform.name == "Player1")
            handleInputP1();
        else
            handleInputP2();



    }

    private void handleInputP2()
    {
        float h;

        if ((h = Input.GetAxisRaw("Horizontal1")) != 0)
        {
            if (isGrounded)
                body.AddForce(-h * transform.forward * impulseForce, ForceMode.Acceleration); //TODO review force mode
            else
                body.AddTorque(-h * transform.right * turnRate, ForceMode.Acceleration);
        }
        if (Input.GetButtonDown("Jump1"))
        {
            jump();
        }
        if (Input.GetButton("Boost1") && boost > 0)
        {
            boostController(1);
        }
        if (Input.GetButtonUp("Boost1") || boost <= 0)
        {
            boostController(0);
        }
    }

    private void handleInputP1()
    {
        float h;

        if ((h = Input.GetAxisRaw("Horizontal")) != 0)
        {
            if (isGrounded)
                body.AddForce(h*transform.forward * impulseForce, ForceMode.Acceleration); //TODO review force mode
            else
                body.AddTorque(h*transform.right * turnRate, ForceMode.Acceleration);
        }
        if (Input.GetButtonDown("Jump"))
        {
            jump();
        }
        if (Input.GetButton("Boost") && boost > 0)
        {
            boostController(1);
        }
        if (Input.GetButtonUp("Boost") || boost <= 0)
        {
            boostController(0);

        }
    }

    private void boostController(int i)
    {
        if (i == 0)
        {
            ps.Stop();
        }
        else
        {
            boost -= 1.0f; //TODO boost rate
            ps.Play();
            body.AddForce(transform.forward * boostForce, ForceMode.Acceleration);
        }
    }

    private void jump() 
    {
        if (isGrounded)
        {
            jumpCount = 1;
            body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        else if(jumpCount  <= 1) {
            body.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    void isGroundedCheck()
    {
        isGrounded = Physics.Raycast(transform.position, -transform.up, 0.45f);
    } //TODO while !isGrounded trail renderer



}
