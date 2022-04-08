using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimManager : MonoBehaviour {

    private Rigidbody2D rb2d;

    [SerializeField] private int speed;
    [SerializeField] private float delayBeforeDestroy;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(0, speed);
        Destroy(gameObject, delayBeforeDestroy);
	}
	

}
