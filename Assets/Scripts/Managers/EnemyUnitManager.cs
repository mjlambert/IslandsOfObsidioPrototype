using UnityEngine;
using System.Collections;

public class EnemyUnitManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnEnemyUnit(Vector3 position)
    {
        Instantiate(Resources.Load(@"Prefabs/Units/EnemyUnit"), position, Quaternion.identity);
    }
}
