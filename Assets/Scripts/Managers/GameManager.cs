using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    private EnemyUnitManager enemyUnitManager;
    private GameUIManager gameUIManager;

    // Use this for initialization
    void Start ()
    {
        enemyUnitManager = GetComponentInParent<EnemyUnitManager>();
        if (enemyUnitManager == null)
        {
            Debug.LogError("Enemy Unit Manager could not be found.");
        }

        gameUIManager = GetComponentInParent<GameUIManager>();
        if (gameUIManager == null)
        {
            Debug.LogError("Game UI Manger could not be found.");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        float timeLeftInWave = enemyUnitManager.TimeLeftInCurrentWave();
        gameUIManager.SetTimeLeftInWaveText(string.Format("Time Left: {0}", Mathf.RoundToInt(timeLeftInWave)));

        int waveNumber = enemyUnitManager.WaveNumber();
        gameUIManager.SetWaveNumber(string.Format("Wave {0}", waveNumber));

        if (enemyUnitManager.WaveCompleted() && enemyUnitManager.WavesLeft() > 0)
        {
            enemyUnitManager.StartWave();
        }
	}
}
