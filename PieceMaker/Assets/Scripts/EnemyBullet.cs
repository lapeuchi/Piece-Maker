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
    }

    void Update()
    {
        transform.position += Time.deltaTime * speed * dir;
    }
}
