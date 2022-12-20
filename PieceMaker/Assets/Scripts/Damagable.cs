using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] EnemyController enemy;
    [SerializeField] float factor;

    private void Awake()
    {
        enemy = GetComponentInParent<EnemyController>();
        gameObject.tag = "Enemy";
    }

    public void Hit(float damage)
    {
        damage *= factor;
        Debug.Log("BossHit " + damage);
        enemy.OnDamaged(damage);
    }
}
