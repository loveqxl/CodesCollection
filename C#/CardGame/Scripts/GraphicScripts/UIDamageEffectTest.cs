using UnityEngine;
using System.Collections;

public class UIDamageEffectTest : MonoBehaviour {

    public GameObject DamagePrefab;
    public static UIDamageEffectTest Instance;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            UIDamageEffect.CreateDamageEffect(transform.position, Random.Range(1, 7));
    }
}
