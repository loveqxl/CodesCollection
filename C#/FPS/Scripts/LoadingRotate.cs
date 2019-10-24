using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotate : MonoBehaviour
{
    [SerializeField]
    private int rotateSpeed=300;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0f,0f,0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, rotateSpeed * Time.deltaTime), Space.Self);
    }
}
