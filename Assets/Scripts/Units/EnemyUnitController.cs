using UnityEngine;
using System.Collections;

public class EnemyUnitController : MonoBehaviour {

    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetDestination(Waypoint.FindWaypointPosition(1));
    }

    /// <summary>
    /// Called when this unit enters a waypoint.
    /// </summary>
    /// <param name="order">Waypoint order.</param>
    public void OnWaypointEnter(int order)
    {
        Vector3 nextWaypointPosition = Waypoint.FindWaypointPosition(order + 1);
        SetDestination(nextWaypointPosition);
    }

    /// <summary>
    /// Set destination on the nav mesh agent
    /// </summary>
    /// <param name="target">Location of destination.</param>
    public void SetDestination(Vector3 target)
    {
        navMeshAgent.SetDestination(target);
    }

    /// <summary>
    /// Damage Enemy Unit
    /// </summary>
    /// <param name="damage">Amount of Damage</param>
    public void DamageUnit(float damage)
    {

    }
}
