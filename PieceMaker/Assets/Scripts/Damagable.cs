using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damagable : MonoBehaviour
{
    [SerializeField] Boss boss;
    [SerializeField] float factor;

    private void Awake()
    {
        GameObject.Find("Boss").GetComponent<Boss>();
    }

    public void Hit(float damage)
    {
        damage *= factor;
        Debug.Log("BossHit " + damage);
        boss.Hit(damage);   
    }
}
