using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour {

    public GameObject[] buffs = new GameObject[4];

    private Vector3[] mapSpawnPos = new Vector3[16];

    private GameObject currentBuff;

    [SerializeField] private Vector3[] spawnPos = new Vector3[4];

    private bool shouldSpawn;

	void Start ()
    {
        shouldSpawn = true;
        SpawnPosForMap();
        SetSpawnPos();
	}
	
	void Update ()
    {
        Spawn();
	}

    void SetSpawnPos()
    {
        for (int i = 0; i < 4; i++)
        {
            spawnPos[i] = mapSpawnPos[i + PlayerPrefs.GetInt("Map") * 4];
        }
    }

    void SpawnPosForMap()
    {
        //Japanese Map
        mapSpawnPos[0] = new Vector3(0, -3.8f, 0);
        mapSpawnPos[1] = new Vector3(5, -0.35f, 0);
        mapSpawnPos[2] = new Vector3(-4.8f, 1.3f, 0);
        mapSpawnPos[3] = new Vector3(0, 1.3f, 0);

        //Pirate Map
        mapSpawnPos[4] = new Vector3(-4.7f, -0.3f, 0);
        mapSpawnPos[5] = new Vector3(7.5f, -0.3f, 0);
        mapSpawnPos[6] = new Vector3(1.5f, -2.7f, 0);
        mapSpawnPos[7] = new Vector3(-7.8f, -2.7f, 0);

        //Ninja Map
        mapSpawnPos[8] = new Vector3(-4.7f, -0.3f, 0);
        mapSpawnPos[9] = new Vector3(7.5f, -0.3f, 0);
        mapSpawnPos[10] = new Vector3(1.5f, -2.7f, 0);
        mapSpawnPos[11] = new Vector3(-1.5f, -2.7f, 0);

        //Mage Map
        mapSpawnPos[12] = new Vector3(-4.7f, -0.3f, 0);
        mapSpawnPos[13] = new Vector3(7.5f, -0.3f, 0);
        mapSpawnPos[14] = new Vector3(1.5f, -2.7f, 0);
        mapSpawnPos[15] = new Vector3(-1.5f, -2.7f, 0);
    }

    void Spawn()
    {
        if (currentBuff == null)
        {
            if (shouldSpawn)
            {
                StartCoroutine(WaitToSpawn());
            }
        }
    }

    IEnumerator WaitToSpawn()
    {
        shouldSpawn = false;
        yield return new WaitForSeconds(Random.Range(10, 20));
        currentBuff = Instantiate(buffs[Random.Range(0, 4)]);
        currentBuff.transform.position = spawnPos[Random.Range(0, 4)];
        shouldSpawn = true;
    }
}
