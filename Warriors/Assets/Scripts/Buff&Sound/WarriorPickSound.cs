using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorPickSound : MonoBehaviour {

    private AudioSource audioZ;

	void Start ()
    {
        audioZ = GetComponent<AudioSource>();
	}
	

	void Update ()
    {
        if (!audioZ.isPlaying)
        {
            audioZ.enabled = false;
        }
	}
}
