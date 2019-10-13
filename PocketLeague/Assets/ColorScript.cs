using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorScript : MonoBehaviour
{
    // Start is called before the first frame update

    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float exp = 0.6f+Mathf.PingPong(Time.time, 1.5f);
        rend.material.SetFloat("_FresnelExponent", exp);
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
