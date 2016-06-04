using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShootEnemy : MonoBehaviour {

    //To store enemies in range
    public List<GameObject> enemiesInRange;

    public GameObject target;
    public float RotationSpeed;
    private Quaternion lookRotation;
    private Vector3 direction;

    // Use this for initialization
    void Start ()
    {
        //empty list on start
        enemiesInRange = new List<GameObject>();

        target = null;
        lookRotation = new Quaternion();
        direction = new Vector3();

	}
	
    //Remove enemy from enemiesInRange
    void OnEnemyDestroy (GameObject enemy)
    {
        enemiesInRange.Remove(enemy);
    }

    //Add enemy to list of enemiesInRange on trigger enter
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag.Equals("EnemyUnits"))
        {
            enemiesInRange.Add(other.gameObject);

            target = other.gameObject;    
        }
    }

    //Remove enemy from the list on trigger exit
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag.Equals("EnemyUnits"))
        {
            enemiesInRange.Remove(other.gameObject);

            target = null;
        }
    }

	// Update is called once per frame
	void Update ()
    {

        //Vector3 direction = gameObject.transform.position - target.transform.position;
        //gameObject.transform.rotation = Quaternion.AngleAxis(Mathf.Atan2(direction.z, direction.x) * 180 / Mathf.PI, new Vector3(0, 1, 0));

        direction = (target.transform.position - gameObject.transform.position).normalized;

        lookRotation = Quaternion.LookRotation(direction);

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);
        
    }
}
