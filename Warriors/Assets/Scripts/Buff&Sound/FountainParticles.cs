using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainParticles : MonoBehaviour {

    public GameObject hpParticles;
    public GameObject enParticles;

    private GameObject fountParticles;

    private GameObject parent;

    private float startingTime;

    private bool hpRegen;
    private bool mpRegen;

	void Start ()
    {
        parent = transform.parent.parent.parent.gameObject;
	}
	
	void Update ()
    {
        if (fountParticles != null)
        {
            fountParticles.transform.localPosition = new Vector3(0, 0, 0);
        }
        Regen();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Fountain")
        {
            startingTime = Time.time;
            if (col.transform.name == "HealthFountain")
            {
                fountParticles = Instantiate(hpParticles);
                fountParticles.transform.SetParent(parent.transform);
                hpRegen = true;
            }
            if (col.transform.name == "EnergyFountain")
            {
                fountParticles = Instantiate(enParticles);
                fountParticles.transform.SetParent(transform.parent);
                mpRegen = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.transform.tag == "Fountain")
        {
            Destroy(fountParticles);
            hpRegen = false;
            mpRegen = false;
        }
    }

    void Regen()
    {
        if (hpRegen)
        {
            if (Time.time - startingTime > 0.8f)
            {
                parent.SendMessage("GetHealth", 10);
                startingTime = Time.time;
            }
        }
        if (mpRegen)
        {
            if (Time.time - startingTime > 0.8f)
            {
                parent.SendMessage("GetEnergy", 10);
                startingTime = Time.time;
            }
        }
        
    }
}
