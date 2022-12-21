using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float damage = 20;
    float speed = 20f;

    void Start()
    {
        Destroy(gameObject, 20f);
    }
    
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            Damagable damagable = other.gameObject.GetComponent<Damagable>();
            if(damagable != null)
                damagable.Hit(damage);
            GetComponent<ExplosionObject>().Explosion();
            Destroy(gameObject);
            
        }
    }
}
