using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerUnitController : MonoBehaviour
{


    public float RotationSpeed;
    public float damagePerSecond;
    public float fireRate;


    //To store enemies in range
    private List<GameObject> enemiesInRange;
    private GameObject target;
    private Quaternion lookRotation;
    private Vector3 direction;
    private float fireTimer;

    // Use this for initialization
    void Start()
    {
        // initializing targetting
        enemiesInRange = new List<GameObject>();
        target = null;
        lookRotation = new Quaternion();
        direction = new Vector3();
        fireTimer = 0;
    }

    // Remove enemy from enemiesInRange
    void OnEnemyDestroy(GameObject enemy)
    {
        
    }

    // Add enemy unit to the list when it enters the trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("EnemyUnits"))
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    //Remove enemy from the list when it leaves the trigger
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("EnemyUnits"))
        {
            enemiesInRange.Remove(other.gameObject);

            if (other.gameObject == target)
            {
                Behaviour halo = (Behaviour)target.GetComponent("Halo");
                halo.enabled = false;
                target = null;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Aquire target
        while (target == null && enemiesInRange.Count > 0)
        {
            target = enemiesInRange[enemiesInRange.Count - 1];

            // Enemy might have been destroyed, so remove it from list
            if (target == null)
            {
                enemiesInRange.Remove(target);
            }
            else
            {
                // Highlight target (temporary)
                Behaviour halo = (Behaviour)target.GetComponent("Halo");
                halo.enabled = true;
            }
        }

        if (target != null)
        {
            //animation too look nice
            direction = (target.transform.position - gameObject.transform.position).normalized;
            lookRotation = Quaternion.LookRotation(direction);
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * RotationSpeed);

            // Damage target
            EnemyUnitController enemyUnit = target.GetComponent<EnemyUnitController>();
            if (enemyUnit == null)
            {
                Debug.LogError("Could not find EnemyUnitController on target.");
            }
            else
            {
                fireTimer += Time.deltaTime;
                if (fireTimer >= fireRate)
                {
                    fireTimer = 0;
                    enemyUnit.DamageUnit(damagePerSecond * fireRate);
                }
            }
        }

    }
}
