using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour {

    private string Control;
    private GetKeyPressed getK;
    private int playerNumber;

    private enum standName { Sam, Musk, Viking, Pirate, Ninja, Hoplite, Crusader, Knight }

    private standName stdN;

	void Start ()
    {
        playerNumber= transform.parent.GetComponent<ControllerForCharacterSelection>().PlayerNumber;
        Control = transform.parent.GetComponent<ControllerForCharacterSelection>().Control;
        getK = GameObject.Find("GetButtonPressed").GetComponent<GetKeyPressed>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.name == "Exit")
        {
            if (Input.GetButtonDown("Attack" + Control))
            {
                Destroy(transform.parent.parent.parent.gameObject);
            }
        }
        if (col.transform.tag == "Stand")
        {
            if (Input.GetButtonDown("Attack" + Control))
            {
                if (!getK.PlayersReady[playerNumber])
                {
                    stdN = (standName)System.Enum.Parse(typeof(standName), col.transform.name);
                    col.GetComponent<AudioSource>().enabled = true;
                    getK.PlayerChild[playerNumber] = (int)stdN;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.transform.name == "Exit")
        {
            if (Input.GetButtonDown("Attack" + Control))
            {
                Destroy(transform.parent.gameObject);
            }
        }
        if (col.transform.tag == "Stand")
        {
            if (Input.GetButtonDown("Attack" + Control))
            {
                if (!getK.PlayersReady[playerNumber])
                {
                    stdN = (standName)System.Enum.Parse(typeof(standName), col.transform.name);
                    col.GetComponent<AudioSource>().enabled = true;
                    getK.PlayerChild[playerNumber] = (int)stdN;
                }
            }
        }
    }
}
