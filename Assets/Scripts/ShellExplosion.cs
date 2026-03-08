using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask tankMask;
    public ParticleSystem explosionParticle;

    public AudioSource explosionAudio;
    public float maxDamage = 100f;
    public float explosionForce = 1000f;
    public float maxLifeTime = 2f;
    public float explostionRadius = 5f;
    void Start()
    {
        Destroy(gameObject,maxLifeTime);
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explostionRadius,tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidBobody = colliders[i].GetComponent<Rigidbody>();

            if(!targetRigidBobody)
                continue;

            targetRigidBobody.AddExplosionForce(explosionForce, transform.position, explostionRadius);
        }

        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
        Destroy(gameObject);
    }
}
