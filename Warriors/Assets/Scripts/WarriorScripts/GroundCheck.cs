using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private bool grounded;

    public bool GetGrounded()
    {
        return grounded;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Floor" || col.transform.tag == "Platform")
        {
            grounded = true;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "Floor" || col.transform.tag == "Platform")
        {
            grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Floor" || col.transform.tag == "Platform")
        {
            grounded = false;
        }
    }
}
