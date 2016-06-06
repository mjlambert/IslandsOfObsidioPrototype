using UnityEngine;
using System.Collections;

public class EnemyUnitManager : MonoBehaviour {

    /// <summary>
    /// Location to spawn enemy units.
    /// </summary>
    public Vector3 enemyUnitSpawnLocation;

    /// <summary>
    /// Spawn Rate of the enemy units.
    /// </summary>
    public float spawnRate;
    
    /// <summary>
    /// Types of enemy units.
    /// </summary>
    public enum EnemyUnit
    {
        standard
    }
    
    /// <summary>
    /// Enemy unit to spawn in current wave.
    /// </summary>
    private EnemyUnit currentEnemyUnit;

    /// <summary>
    /// Number of units to spawn in current wave.
    /// </summary>
    private int waveSpawnLimit;

    /// <summary>
    /// Enemy units spawned in current wave.
    /// </summary>
    private int enemyUnitsSpawned;

	void Start ()
    {
        currentEnemyUnit = EnemyUnit.standard;
        waveSpawnLimit = 100;
        enemyUnitsSpawned = 0;
	}
	
	void Update ()
    {
	    // If current wave is over, stop spawning enemy units.
        if (enemyUnitsSpawned >= waveSpawnLimit)
        {
            CancelInvoke("SpawnEnemyUnit");
        }
	}

    /// <summary>
    /// Start a Wave.
    /// </summary>
    /// <param name="unit">Enemy unit to spawn in wave.</param>
    /// <param name="spawnLimit">Number of enemy units to spawn.</param>
    public void StartWave(EnemyUnit unit, int spawnLimit)
    {
        currentEnemyUnit = unit;
        waveSpawnLimit = spawnLimit;
        InvokeRepeating("SpawnEnemyUnit", spawnRate, spawnRate);
    }

    /// <summary>
    /// Spawns the current enemy unit at the enemy unit spawn location/
    /// </summary>
    private void SpawnEnemyUnit()
    {
        Instantiate(Resources.Load(GetUnitResource(currentEnemyUnit)), enemyUnitSpawnLocation, Quaternion.identity);
        enemyUnitsSpawned++;
    }

    /// <summary>
    /// Maps enemy unit type to resource location.
    /// </summary>
    /// <param name="unit">Enemy unit.</param>
    private string GetUnitResource(EnemyUnit unit)
    {
        switch (unit)
        {
            case EnemyUnit.standard: return @"Prefabs/Units/EnemyUnit";
            default: return @"Prefabs/Units/EnemyUnit";
        }
    }

}
