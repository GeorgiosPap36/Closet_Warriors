using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDeactivate : MonoBehaviour {


	void Start ()
    {
        if (PlayerPrefs.GetInt(transform.name + "Toggle") == 0)
        {
            gameObject.SetActive(false);
        }
	}

}
