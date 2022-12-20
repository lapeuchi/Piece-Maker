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

    private void Update()
    {
        transform.Translate(Vector3.forward * 5 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            other.GetComponent<EnemyController>().OnDamaged(damage);
        }
    }
}
