using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFire : MonoBehaviour {


    void Awake()
    {
        StartCoroutine(DestoyPart());
    }

    IEnumerator DestoyPart()
    {
        yield return new WaitForSeconds(7);
        Destroy(transform.parent.gameObject);
        Debug.Log(1);
    }
}
