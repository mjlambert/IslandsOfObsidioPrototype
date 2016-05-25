using UnityEngine;
using System.Collections;

public class EnemyUnitController : MonoBehaviour {

    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination(Waypoint.FindWaypointPosition(1));
    }

    public void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }
}
