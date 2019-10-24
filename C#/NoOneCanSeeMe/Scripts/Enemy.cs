using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent enemyAgent;

    public Transform[] patrolPoints;
    private int destPoint = 0;

    public Transform target;
    public float range = 5f;
    public string targetTag = "Player";
    // Start is called before the first frame update
    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>();

        InvokeRepeating("UpdateTarget", 0f, 0.5f);

        enemyAgent.autoBraking = false;
        Patrol();
    }


    void Patrol() {
        if (patrolPoints.Length == 0) return;

        enemyAgent.destination = patrolPoints[destPoint].position;

        destPoint = (destPoint + 1) % patrolPoints.Length;
    }

    void UpdateTarget() {
        GameObject[] objectsInRange = GameObject.FindGameObjectsWithTag("Player");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestObj = null;

        foreach (GameObject obj in objectsInRange) {
            float distance = Vector3.Distance(transform.position, obj.transform.position);

            if (distance < shortestDistance) {
                shortestDistance = distance;
                nearestObj = obj;
            }
        }

        if (nearestObj != null && shortestDistance <= range)
        {

            target = nearestObj.transform;
        }
        else {
            target = null;
        }
    }

   void Update()
    {
        if (target == null|| target.gameObject.layer != LayerMask.NameToLayer("Marked")) {
            if (!enemyAgent.pathPending && enemyAgent.remainingDistance < 0.5f) {
                Patrol();
            }
            return;
        }
            

        if (target!=null&&target.gameObject.layer == LayerMask.NameToLayer("Marked"))
        {
            enemyAgent.SetDestination(target.position);
            
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }

}
