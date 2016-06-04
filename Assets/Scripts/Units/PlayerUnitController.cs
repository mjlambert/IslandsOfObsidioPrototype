using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerUnitController : MonoBehaviour
{

    //To store enemies in range
    public List<GameObject> enemiesInRange;

    public GameObject target;
    public float RotationSpeed;
    private Quaternion lookRotation;
    private Vector3 direction;

    // Use this for initialization
    void Start()
    {


        //initializing targetting
        enemiesInRange = new List<GameObject>();
        target = null;
        lookRotation = new Quaternion();
        direction = new Vector3();

    }

    //Remove enemy from enemiesInRange
    void OnEnemyDestroy(GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    //Add enemy to list of enemiesInRange on trigger enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("EnemyUnits"))
        {
            enemiesInRange.Add(other.gameObject);

            //target = other.gameObject; //just to have a target Change to last in list   
        }
    }

    //Remove enemy from the list on trigger exit
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("EnemyUnits"))
        {
            if (other.gameObject == target)
            {
                enemiesInRange.Remove(other.gameObject);

                Behaviour halo = (Behaviour)target.GetComponent("Halo");
                halo.enabled = false;
                target = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null && enemiesInRange.Count > 0)
        {
            target = enemiesInRange[enemiesInRange.Count - 1];

            Behaviour halo = (Behaviour)target.GetComponent("Halo");
            halo.enabled = true;

            //animation too look nice
            direction = (target.transform.position - gameObject.transform.position).normalized;

            lookRotation = Quaternion.LookRotation(direction);

            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        }

    }
}
