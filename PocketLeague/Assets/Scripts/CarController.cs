using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private float impulseForce = 20f;
    private float jumpForce = 300f;
    private float boostForce = 25f;
    private float turnRate = 30f;
    private float flipForce = 1000f;
    private float boostDischargeRate = 60f;
    private float boostChargeRate = 20f;

    private bool isGrounded;
    private bool isFlipped;
    private Rigidbody body;
    
    private float jumpCD;
    private int jumpCount;
    private ParticleSystem ps;
    private TrailRenderer tr;
    private float boost;

    // IMPORTANT NOTE:
    // KeyCode thisKeyCode = (KeyCode) System.Enum.Parse(typeof(KeyCode), "Whatever")
    // converts string from the player prefs back to keycode so that we can use it in the controls
    //

    // Start is called before the first frame update

    // Controls
    KeyCode forwardKey;
    KeyCode backKey;
    KeyCode leftKey;
    KeyCode rightKey;
    KeyCode jumpKey;
    KeyCode nitroKey;
    void Start()
    {
        SetupControls();

        body = GetComponent<Rigidbody>();
        ps = GetComponent<ParticleSystem>();
        tr = GetComponent<TrailRenderer>();

        boost = 100;
        jumpCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        isGroundedCheck();

        if (transform.name == "Player1")
            //handleInputP1();
            handleCustomInput();
        else
            handleInputP2();

        boost = Mathf.Clamp(boost + (boostChargeRate * Time.deltaTime),0,100);
        if (!isFlipped && !isGrounded)
            tr.emitting = true;
        else
            tr.emitting = false;

    }

    
    private void handleInputP2()
    {
        float h;

        if ((h = Input.GetAxisRaw("Horizontal1")) != 0)
        {
            if (isGrounded)
                body.AddForce(-h * transform.forward * impulseForce, ForceMode.Acceleration); 
            else if (isFlipped)
                body.AddTorque(-h * transform.right * flipForce, ForceMode.VelocityChange);
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
                body.AddForce(h * transform.forward * impulseForce, ForceMode.Acceleration);
            else if (isFlipped)
                body.AddTorque(h * transform.right * flipForce, ForceMode.VelocityChange);
            else
                body.AddTorque(h * transform.right * turnRate, ForceMode.Acceleration);
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

    private void handleCustomInput()
    {
        float h;

        if (Input.GetKey(forwardKey))
        {
            body.AddForce(transform.forward * impulseForce, ForceMode.Acceleration);
        } 
        if (Input.GetKey(backKey))
        {
            body.AddForce(-transform.forward * impulseForce, ForceMode.Acceleration);
        }
        if (Input.GetKey(leftKey))
        {
            if (isGrounded)
                body.AddTorque(-transform.right * turnRate, ForceMode.Acceleration);
            else
                body.AddTorque(-transform.right * flipForce, ForceMode.VelocityChange);
        }
        if (Input.GetKey(rightKey))
        {
            if (isGrounded)
                body.AddTorque(transform.right * turnRate, ForceMode.Acceleration);
            else
                body.AddTorque(transform.right * flipForce, ForceMode.VelocityChange);
        }
        if (Input.GetKey(jumpKey))
        {
            jump();
        }
        if (Input.GetKey(nitroKey) && boost > 0)
        {
            boostController(1);
        } else
        {
            boostController(0);
        }

        //if ((h = Input.GetAxisRaw("Horizontal")) != 0)
        //{
        //    if (isGrounded)
        //        body.AddForce(h * transform.forward * impulseForce, ForceMode.Acceleration);
        //    else if (isFlipped)
        //        body.AddTorque(h * transform.right * flipForce, ForceMode.VelocityChange);
        //    else
        //        body.AddTorque(h * transform.right * turnRate, ForceMode.Acceleration);
        //}
        //if (Input.GetButtonDown("Jump"))
        //{
        //    jump();
        //}
        //if (Input.GetButton("Boost") && boost > 0)
        //{
        //    boostController(1);
        //}
        //if (Input.GetButtonUp("Boost") || boost <= 0)
        //{
        //    boostController(0);

        //}
    }

    private void boostController(int i)
    {
        if (i == 0)
        {
            ps.Stop();
        }
        else
        {
            boost -= boostDischargeRate*Time.deltaTime; 
            ps.Play();
            body.AddForce(transform.forward * boostForce, ForceMode.Acceleration);
        }
    }

    public void resetBoost()
    {
        boost = 100;
    }

    public float getBoost()
    {
        return boost;
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
        isFlipped = Physics.Raycast(transform.position, transform.up, 0.40f);
        //Debug.DrawRay(transform.position, transform.up* 0.40f);
    }

    private void SetupControls()
    {
        forwardKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Constants.forwardKey, ""));
        backKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Constants.backKey, ""));
        leftKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Constants.leftKey, ""));
        rightKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Constants.rightKey, ""));
        jumpKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Constants.jumpKey, ""));
        nitroKey = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(Constants.NitroKey, ""));
    }

}
