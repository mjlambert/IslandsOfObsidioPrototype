using UnityEngine;
using System.Collections;

public class EnemyUnitManager : MonoBehaviour {

    /// <summary>
    /// Location to spawn enemy units.
    /// </summary>
    public Vector3 enemyUnitSpawnLocation;

    /// <summary>
    /// Enemy waves that will be carried out this round.
    /// </summary>
    public EnemyWave[] waves;

    // Current wave information
    private bool currentWaveCompleted;
    private int currentWaveIndex;
    private float currentWaveTimeLeft;
    private EnemyWave currentWave;
    private int wavesLeft;

    /// <summary>
    /// Enemy units spawned in current wave.
    /// </summary>
    private int enemyUnitsSpawned;

	void Start ()
    {
        currentWaveCompleted = true;
        currentWaveIndex = 0;
        enemyUnitsSpawned = 0;
        wavesLeft = waves.Length;
        
        if (waves.Length > 0)
        {
            currentWave = waves[currentWaveIndex];
            currentWaveTimeLeft = currentWave.timeLimit;
        }
        else
        {
            Debug.LogError("No enemy waves.");
        }
	}
	
	void Update ()
    {
        // If we've hit the spawn limit, stop spawning enemy units.
        if (enemyUnitsSpawned >= currentWave.spawnLimit)
        {
            //Debug.Log(string.Format("Spawned {0} units. Stopping spawning.", enemyUnitsSpawned));
            CancelInvoke("SpawnEnemyUnit");
        }

        // Count down the round if it has not completed.
        if (!currentWaveCompleted) currentWaveTimeLeft -= Time.deltaTime;

        // If there is no time left, finish the wave, and initialise the next wave.
        if (currentWaveTimeLeft <= 0 && !currentWaveCompleted)
        {
            Debug.Log(string.Format("Wave {0} complete!", currentWaveIndex + 1));

            enemyUnitsSpawned = 0;
            currentWaveCompleted = true;
            currentWaveIndex++;
            wavesLeft--;

            if (currentWaveIndex < waves.Length)
            {
                currentWave = waves[currentWaveIndex];
                currentWaveTimeLeft = currentWave.timeLimit;
            }
            else
            {
                Debug.Log("All waves complete.");
            }
        }
	}

    /// <summary>
    /// Start the next wave.
    /// </summary>
    public void StartWave()
    {
        if (wavesLeft == 0)
        {
            Debug.LogError("No more waves to start.");
            return;
        }

        if (waves.Length > 0)
        {
            Debug.Log(string.Format("Starting Wave {0}. Spawning {1} instances of {2} at a rate of {3} seconds.",
                currentWaveIndex + 1,
                currentWave.spawnLimit,
                currentWave.enemyUnit.name,
                currentWave.spawnRate));
            currentWaveCompleted = false;
            InvokeRepeating("SpawnEnemyUnit", currentWave.spawnRate, currentWave.spawnRate);
        }
        else
        {
            Debug.LogError("No enemy waves.");
            return;
        }
    }

    public int WaveNumber()
    {
        return currentWaveIndex + 1;
    }

    public int WavesLeft()
    {
        return wavesLeft;
    }

    public float TimeLeftInCurrentWave()
    {
        return currentWaveTimeLeft;
    }

    public bool WaveCompleted()
    {
        return currentWaveCompleted;
    }

    /// <summary>
    /// Spawns the current enemy unit at the enemy unit spawn location
    /// </summary>
    private void SpawnEnemyUnit()
    {
        Instantiate(currentWave.enemyUnit, enemyUnitSpawnLocation, Quaternion.identity);
        enemyUnitsSpawned++;
    }

}

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyUnit;
    public float spawnRate;
    public int spawnLimit;
    public int timeLimit;
}
