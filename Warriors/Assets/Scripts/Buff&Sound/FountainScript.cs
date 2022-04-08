using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainScript : MonoBehaviour {

    private GameObject waterParticles;

    private bool active;

	void Start ()
    {
        waterParticles = transform.GetChild(0).gameObject;
        active = true;
	}
	
	void Update ()
    {
        waterParticles.SetActive(active);
	}
}
