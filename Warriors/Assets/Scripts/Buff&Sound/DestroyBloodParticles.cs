using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBloodParticles : MonoBehaviour {


    private ParticleSystem pS;

	void Start ()
    {
        pS = GetComponent<ParticleSystem>();
	}
	

	void Update ()
    {
        if (!pS.IsAlive())
        {
            Destroy(gameObject);
        }
	}
}
