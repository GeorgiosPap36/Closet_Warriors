using UnityEngine;
using System.Collections;

public class SetSortingLayer : MonoBehaviour
{
    public string layerToPushTo;

    void Start()
    {
        layerToPushTo = "Foreground";
        GetComponent<Renderer>().sortingLayerName = layerToPushTo;
    }
}