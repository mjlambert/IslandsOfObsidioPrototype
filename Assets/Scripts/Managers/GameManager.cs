using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    // Temporary!
    const int waveLimit = 100;
    int numberOfWaves = 0;

    //public
    public GameObject Selected;

    public Vector3 MovePos;
    
    // Use this for initialization
	void Start ()
    {
        InvokeRepeating("SpawnWave", 2f, 1f);
        Selected = null;

        
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (numberOfWaves >= waveLimit)
        {
            CancelInvoke("SpawnWave");
        }

        //on left mouse button down do select unit
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }

        if (Input.GetMouseButtonDown(1))
        {
            MoveUnit();
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

        int numberOfUnitsPerWave = 1;

        for (int i = 0; i < numberOfUnitsPerWave; i++)
        {
            Instantiate(Resources.Load(@"Prefabs/Units/EnemyUnit"), spawnLocations[i], Quaternion.identity);
        }
    }

    void SelectUnit()
    {
        //cast a ray from the camera to the mouse position
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

        Debug.Log("Mouse is down");

        if(hit)
        {
            Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            //compare if the tag is playerunit
            if(hitInfo.transform.gameObject.tag == "PlayerUnit")
            {
                Debug.Log("Hit with tag: PlayerUnit");
               Selected = hitInfo.collider.gameObject;
            }
            else
            {
                Debug.Log("not working");
                Selected = null;                
            }
        }
        else
        {
            Debug.Log("No Hit");
        }

    }

    void MoveUnit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Plane hPlane = new Plane(Vector3.up, Vector3.zero);

        float distance = 0;

        if (hPlane.Raycast(ray, out distance))
        {
            MovePos = ray.GetPoint(distance);
        }

    }
    

}
