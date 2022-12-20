using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionObject : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    public void Explosion()
    {
        ParticleSystem go = Instantiate(particle, transform.position, transform.rotation);
        go.playOnAwake = false;
        go.loop = false;
        Destroy(go, 4f);
        particle.Play();
    }
}
