using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage = 5;
    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Bullet"))
        {
            other.GetComponent<EnemyController>().OnDamaged(5);
        }
    }
}
