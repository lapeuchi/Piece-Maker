using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    Transform target;
    Vector3 dir;
    float speed = 5f;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        dir = (target.position - transform.position).normalized;
        dir.y = 0;
        Destroy(gameObject, 20f);
    }

    void Update()
    {
        transform.position += Time.deltaTime * speed * dir;

        transform.rotation = Quaternion.LookRotation(dir.normalized);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit");
            gameObject.GetComponent<ExplosionObject>().Explosion();
            Destroy(gameObject);
            other.gameObject.GetComponent<PlayerController>().Hit();
        }
    }
}
