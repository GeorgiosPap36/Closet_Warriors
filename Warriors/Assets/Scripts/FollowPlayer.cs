using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {


    private GameObject player;

	
	void Update ()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
        }	
	}

    void SetPlayer(GameObject pl)
    {
        player = pl;
    }
}
