using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageMapPlatforms : MonoBehaviour {

    private PlatformController platC;


    void Start ()
    {
        platC = GetComponent<PlatformController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    { 
        if (col.transform.name == "ChangeDirection")
        {
            platC.move = (-1) * platC.move;
        }
    }
}
