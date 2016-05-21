using UnityEngine;
using System.Collections;

public class EnemyUnitController : MonoBehaviour {

    /*
    private NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();
        navMeshAgent.CalculatePath(GameObject.Find("SouthWestTrigger").GetComponent<Transform>().position, path);
        navMeshAgent.SetPath(path);
    }

    // Update is called once per frame
    void Update()
    {

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
    */

    NavMeshAgent navMeshAgent; //reference to nav mesh agent
    Transform northWest, southWest, southEast, northEast; //reference to waypoint locations
    bool NW_Hit, SW_Hit, SE_Hit, NE_Hit; //refence for waypoint collision
    
    void Awake()
    {
        northWest = GameObject.FindGameObjectWithTag("WaypointNW").transform;
        southWest = GameObject.FindGameObjectWithTag("WaypointSW").transform;
        southEast = GameObject.FindGameObjectWithTag("WaypointSE").transform;
        northEast = GameObject.FindGameObjectWithTag("WaypointNE").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        //SE_Hit = true;
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == northWest)
        {
            NW_Hit = true;
            NE_Hit = false;
        }

        if (other.gameObject == southWest)
        {
            SW_Hit = true;
            NE_Hit = false;
        }

        if (other.gameObject == southEast)
        {
            SE_Hit = true;
            SW_Hit = false;
        }

        if (other.gameObject == northEast)
        {
            NE_Hit = true;
            SE_Hit = false;
        }
    }

    void Update()
    {
        if (NW_Hit == true)
        {
            navMeshAgent.SetDestination(southWest.position);
        }

        if (SW_Hit == true)
        {
            navMeshAgent.SetDestination(southEast.position);
        }

        if (SE_Hit == true)
        {
            navMeshAgent.SetDestination(northEast.position);
        }

        if (NE_Hit == true)
        {
            navMeshAgent.SetDestination(northWest.position);
        }

    }
}
