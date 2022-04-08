using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonManager : MonoBehaviour {

    private Animator anim;

    private float offset;
    private float speed;

	void Start ()
    {
        anim = GetComponent<Animator>();
        offset = Random.Range(0, 2);
        speed = Random.Range(0.75f, 1);
	}
	

	void Update ()
    {
        anim.SetFloat("Offset", offset);
        anim.SetFloat("Speed", speed);
    }
}
