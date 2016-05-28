using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Temporary!
    const int waveLimit = 100;
    int numberOfWaves = 0;
    
    // Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnWave", 2f, 1f);
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (numberOfWaves >= waveLimit)
        {
            CancelInvoke("SpawnWave");
        }
	}

    // This is just a test. This logic should be moved to an enemy unit manager
    void SpawnWave()
    {
        numberOfWaves++;

        Vector3 spawnOrigin = new Vector3(44, 0, -55);
        List<Vector3> spawnLocations = new List<Vector3>()
        {
            { new Vector3(spawnOrigin.x + 2, spawnOrigin.y, spawnOrigin.z) },
            { new Vector3(spawnOrigin.x + 1, spawnOrigin.y, spawnOrigin.z) },
            { spawnOrigin },
            { new Vector3(spawnOrigin.x - 1, spawnOrigin.y, spawnOrigin.z) },
            { new Vector3(spawnOrigin.x - 2, spawnOrigin.y, spawnOrigin.z) }
        };

        int numberOfUnitsPerWave = 5;

        for (int i = 0; i < numberOfUnitsPerWave; i++)
        {
            Instantiate(Resources.Load(@"Prefabs/Units/EnemyUnit"), spawnLocations[i], Quaternion.identity);
        }
    }

}
