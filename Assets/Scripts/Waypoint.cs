using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

    public int order;

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyUnits")
        {
            EnemyUnitController enemyUnitController;
            try
            {
                enemyUnitController = other.GetComponentInParent<EnemyUnitController>();
                Vector3 nextWaypointPosition = FindWaypointPosition(order + 1);
                enemyUnitController.SetDestination(nextWaypointPosition);
            }
            catch (UnityException exception)
            {
                // TODO: Probably should throw this exception
                Debug.Log("No EnemyUnitController script found. This object should not have the 'EnemyUnits' tag. " + exception.Message);
            }
        }
    }

    /// <summary>
    /// Find the position of a waypoint with the specified order.
    /// </summary>
    /// <param name="order">Order of the waypoint to find.</param>
    /// <returns>Position of the waypoint.</returns>
    public static Vector3 FindWaypointPosition(int order)
    {   
        // Search for waypoints
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Waypoints"))
        {
            // Try to get Waypoint script component
            Waypoint waypointScript = null;
            try
            {
                waypointScript = gameObject.GetComponent<Waypoint>();
            }
            catch (UnityException exception)
            {
                // TODO: Probably should throw this exception
                Debug.Log("No Waypoint script found. This object should not have the 'Waypoints' tag. " + exception.Message);
            }

            // If waypoint matches the order, return its position
            if (waypointScript != null && waypointScript.order == order)
            {
                return gameObject.GetComponent<Transform>().position;
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
