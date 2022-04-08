using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewFadeInOut : MonoBehaviour {

    private SpriteRenderer sprtRend;

    [SerializeField] private float opacity;

    [SerializeField] private bool touching;


	void Start ()
    {
        sprtRend = GetComponent<SpriteRenderer>();
        opacity = 0;
        touching = false;
	}

    private void Update()
    {
        if (touching)
        {
           opacity =  Mathf.Clamp(opacity + 0.1f, 0, 1);
        }
        else
        {
            opacity = Mathf.Clamp(opacity - 0.1f, 0, 1);
        }
        sprtRend.color = new Color(1, 1, 1, opacity);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            touching = true;   
        }

    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Player")
        {
            touching = false;
        }
    }

}
