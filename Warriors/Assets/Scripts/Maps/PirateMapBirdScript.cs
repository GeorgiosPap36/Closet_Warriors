using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateMapBirdScript : MonoBehaviour {

    private Transform right;

    private PlatformController platC;

    private Animator anim;

    private float posY;

    [SerializeField] private bool carrying;

    void Start ()
    {
        right = transform.parent.Find("Right");
        anim = GetComponent<Animator>();
        platC = GetComponent<PlatformController>();
        platC.move = new Vector3(-Random.Range(1, 1.75f), 0, 0);
        carrying = false;
        posY = transform.position.y;
	}

	void Update ()
    {
        anim.SetBool("Carrying", carrying);
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "Left")
        {
            transform.position = new Vector2(right.position.x, posY);
            platC.move = new Vector3(-Random.Range(1, 1.75f), 0, 0);
        }
        if (col.transform.name == "Feet")
        {
            carrying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.name == "Feet")
        {
            carrying = false;
        }
    }

}
