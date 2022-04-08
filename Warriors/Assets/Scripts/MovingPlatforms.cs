using UnityEngine;
using System.Collections;

public class MovingPlatforms : PlatformController
{
    public Transform pos;
    public int onedir;
    private float offset;

    private void Awake()
    {
        Start();
        offset = pos.position.y;
    }

    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x,(offset + onedir * Mathf.PingPong(Time.time, 3)), transform.localPosition.z);
    }
}
