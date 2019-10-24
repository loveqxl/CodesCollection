using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {
    
    private Transform target;

    private Enemy targetEnemy;
    [Header("General")]
    public float range = 15f;
    

    [Header("Use Bullets (default)")]
    public float fireRate = 1f;

    private float fireCountdown = 0f;

    public GameObject bulletprefab;


    [Header("Use Laser")]

    public bool useLaser = false;

    public int damageOverTime = 30;

    public float slowAmount = 0.3f;

    public LineRenderer lineRenderer;

    public ParticleSystem impactEffect;

    public Light impactLight;

    [Header("Unity Setup Fields")]

    public Transform firepoint;

    public string enemyTag = "Enemy";

    public Transform partToRotate;

    public float rotateSpeed = 10f;



	// Use this for initialization
	void Start () {

        InvokeRepeating("UpdateTarget",0f,0.5f);
        
	}
	
	// Update is called once per frame
	void Update () {
        if (target == null) {

            if (useLaser) {
                if (lineRenderer.enabled) {
                    impactEffect.Stop();
                    lineRenderer.enabled = false;
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser) {
            Laser();
        }
        else{

            if (fireCountdown <= 0)
            {

                Shoot();
                fireCountdown = 1f / fireRate;
            }

            fireCountdown -= Time.deltaTime;
        }

       
	}

    void LockOnTarget() {
        //try to understand below (look at Euler Angle , Quaternion , LookRotation() and Lerp())
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * rotateSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void Laser() {

        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowAmount);

        if (!lineRenderer.enabled) {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0,firepoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firepoint.position - target.position;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

        impactEffect.transform.position = target.position + dir.normalized*1f;
    }

    void UpdateTarget() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

        float shortestDistance = Mathf.Infinity;

        GameObject nearestEnemy = null;

        foreach (GameObject badBall in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position,badBall.transform.position);
            if (distanceToEnemy < shortestDistance) {
                shortestDistance = distanceToEnemy;
                nearestEnemy = badBall;
            }

        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = nearestEnemy.GetComponent<Enemy>();
        }
        else {
            target = null;
        }
    }

    void Shoot() {

        GameObject BulletGo = (GameObject)Instantiate(bulletprefab,firepoint.position,firepoint.rotation);
        Bullet bullet = BulletGo.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
