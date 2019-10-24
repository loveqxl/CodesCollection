using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireHydrant : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public float fireRate = 1f;
    public float duration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", Random.Range(1f,4f), 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire() {
        StartCoroutine("Squirt");
    }

    IEnumerator Squirt() {
        float cd = 0f;
        float firetime = 0f;
            while (true) {

            if (cd <= 0)
            {
                SingleFire();
                cd = fireRate;
            }
            else
            {
                cd -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            firetime += Time.deltaTime;
            if (firetime >= duration) {
                firetime = 0;
                StopCoroutine("Squirt");
            }
        }
    }

    private void SingleFire() {
       GameObject blt = (GameObject)Instantiate<GameObject>(bullet,firePoint.position,Quaternion.identity);
        blt.GetComponent<Rigidbody>().AddForce(firePoint.forward*Random.Range(5f,35f),ForceMode.Impulse);
    }
}
