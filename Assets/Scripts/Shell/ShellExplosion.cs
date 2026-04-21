using System;
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
    [Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explostionRadius,tankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidBobody = colliders[i].GetComponent<Rigidbody>();

            if(!targetRigidBobody)
                continue;

            targetRigidBobody.AddExplosionForce(explosionForce, transform.position, explostionRadius);

            TankHealth targetHealth = targetRigidBobody.GetComponent<TankHealth>();

            if(!targetHealth)
                continue;

            float damage = CalculateDamage(targetRigidBobody.position);
            targetHealth.TakeDamage(damage);
        }

        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        explosionAudio.Play();

        Destroy(explosionParticle.gameObject, explosionParticle.duration);
        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        Vector3 exlopsionToTarget = targetPosition - transform.position;

        float explosionDistance = exlopsionToTarget.magnitude;

        float relativeDistance = (explostionRadius - explosionDistance) / explostionRadius;

        //float damage = Mathf.Clamp01(relativeDistance) * maxDamage;

        float damage = Mathf.Max(0f, relativeDistance * maxDamage);
        return damage;
    }
}
