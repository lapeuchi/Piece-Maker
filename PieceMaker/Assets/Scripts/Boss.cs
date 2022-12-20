using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float hp;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Hit(float damage)
    {
        hp -= damage;
        if(hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    { 
        
    }
}
