using UnityEngine;
using System.Collections;

public class EnemyUnit : MonoBehaviour {

    private NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start () {
        navMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(GameObject.Find("SouthWestTrigger").GetComponent<Transform>().position, path);
        navMeshAgent.SetPath(path);
    }
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        Vector3 nextTarget;
        NavMeshPath path = new NavMeshPath();

        switch (other.gameObject.name)
        {
            case "NorthEastTrigger":
                nextTarget = GameObject.Find("NorthWestTrigger").GetComponent<Transform>().position;
                break;
            case "NorthWestTrigger":
                nextTarget = GameObject.Find("SouthWestTrigger").GetComponent<Transform>().position;
                break;
            case "SouthWestTrigger":
                nextTarget = GameObject.Find("SouthEastTrigger").GetComponent<Transform>().position;
                break;
            case "SouthEastTrigger":
                nextTarget = GameObject.Find("NorthEastTrigger").GetComponent<Transform>().position;
                break;
            default:
                nextTarget = GameObject.Find("NorthWestTrigger").GetComponent<Transform>().position;
                break;
        }

        navMeshAgent.CalculatePath(nextTarget, path);
        navMeshAgent.SetPath(path);
    }
}
