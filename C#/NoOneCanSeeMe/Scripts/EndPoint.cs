using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //may need other condition

        if (other.gameObject.tag == "Player" && GameManager.instance.pickups.Count==0) {
            //Debug.Log("Mission Complete!");
            GameManager.instance.Win();
        }
    }
}
