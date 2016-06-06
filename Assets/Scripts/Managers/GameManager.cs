using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Other Managers
    EnemyUnitManager enemyUnitManager;
    
    // Use this for initialization
	void Start ()
    {
        enemyUnitManager = GetComponentInParent<EnemyUnitManager>();
        if (enemyUnitManager == null)
        {
            Debug.LogError("Enemy Unit Manager could not be found.");
        }

        enemyUnitManager.StartWave(EnemyUnitManager.EnemyUnit.standard, 100);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

}
