using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

    public int order;

    private static Dictionary<int, Vector3> _waypointPositionCache = new Dictionary<int, Vector3>();

    void OnTriggerEnter(Collider other)
    {
        // Trigger enemy unit event
        if (other.tag == "EnemyUnits")
        {
            EnemyUnitController enemyUnitController = other.GetComponentInParent<EnemyUnitController>();
            if (enemyUnitController == null)
            {
                Debug.LogError(
                    string.Format("No Enemy Unit Controller script found. This object[{0}] should not have the 'EnemyUnits' tag.",
                    gameObject.name)
                );
            }
            enemyUnitController.OnWaypointEnter(order);
        }
    }

    /// <summary>
    /// Find the position of a waypoint with the specified order.
    /// </summary>
    /// <param name="order">Order of the waypoint to find.</param>
    /// <returns>Position of the waypoint.</returns>
    public static Vector3 FindWaypointPosition(int order)
    {   
        // See if waypoint is in the cache
        if (_waypointPositionCache.ContainsKey(order))
        {
            return _waypointPositionCache[order];
        }
        
        // Search for waypoints
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            // Try to get Waypoint script component
            Waypoint waypointScript = gameObject.GetComponent<Waypoint>();
            if (waypointScript == null)
            {
                Debug.LogError(
                    string.Format("No Waypoint script found. This object[{0}] should not have the 'Waypoints' tag.",
                    gameObject.name)
                );
            }

            // If waypoint matches the order, return its position
            if (waypointScript != null && waypointScript.order == order)
            {
                Vector3 waypointPosition = gameObject.GetComponent<Transform>().position;
                _waypointPositionCache.Add(order, waypointPosition);
                return waypointPosition;
            }
        }

        // No waypoint was found, let's loop to first waypoint
        if (order != 1)
        {
            return FindWaypointPosition(1);
        }
        else
        {
            Debug.Log("Could not find waypoint with order of 1. Defaulting position to (0, 0, 0)");
            return Vector3.zero;
        }
    }

}
