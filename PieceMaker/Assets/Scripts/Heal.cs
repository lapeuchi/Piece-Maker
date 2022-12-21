using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Heal");
            gameObject.GetComponent<ExplosionObject>().Explosion();
            Destroy(gameObject);
            other.GetComponent<PlayerController>().Heal();
        }
    }
}
