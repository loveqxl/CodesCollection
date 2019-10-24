using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float arrestedRange = 1.2f;
    private GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemies=GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in enemies) {
            if (Vector3.Distance(transform.position,obj.transform.position)<=arrestedRange && gameObject.layer==LayerMask.NameToLayer("Marked")) {
                //Debug.Log("Game Over");
                GameManager.instance.Lose();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position+new Vector3(0,0.5f,0),arrestedRange);
    }
}
