using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    
    private EnemyUnitManager enemyUnitManager;
    private GameUIManager gameUIManager;

    //public
    public GameObject Selected;

    public Vector3 MovePos;

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

        
        Selected = null;       
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

        //on left mouse button down do select unit
        if (Input.GetMouseButtonDown(0))
        {
            SelectUnit();
        }

        if (Input.GetMouseButtonDown(1))
        {
            MoveUnit();
        }

        if (Selected != null)
        {
            
        }

    }

    void SelectUnit()
    {
        //cast a ray from the camera to the mouse position
        RaycastHit hitInfo = new RaycastHit();
        bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

        Debug.Log("Mouse is down");

        if (hit)
        {
            Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            //compare if the tag is playerunit
            if (hitInfo.transform.gameObject.tag == "PlayerUnit")
            {
                Debug.Log("Hit with tag: PlayerUnit");
                Selected = hitInfo.collider.gameObject;

                Behaviour halo = (Behaviour)Selected.GetComponent("Halo");
                halo.enabled = true;
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
            Behaviour halo = (Behaviour)Selected.GetComponent("Halo");
            halo.enabled = false;
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
            Debug.Log("Move Position = " + MovePos);

            NavMeshAgent agent = (NavMeshAgent)Selected.GetComponent<NavMeshAgent>();
            agent.destination = MovePos;
        }
    }
}

