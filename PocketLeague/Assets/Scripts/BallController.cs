using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    public float antiGravForce;
    private Rigidbody rb;
    Renderer rend;

    void Start()
    {
        rb = transform.GetComponent<Rigidbody>();
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float exp = 0.6f+Mathf.PingPong(Time.time, 1.5f);
        rend.material.SetFloat("_FresnelExponent", exp);
        rb.AddForce(new Vector3(0,1*antiGravForce,0),ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {

            Color c = collision.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material.GetColor("_EmissionColor");
            rend.material.SetColor("_FresnelColor", c);
        }
    }

}
