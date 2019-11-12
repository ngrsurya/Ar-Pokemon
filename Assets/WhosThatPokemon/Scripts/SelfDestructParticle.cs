using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SelfDestructParticle : MonoBehaviour
{
    private ParticleSystem ps;
    private float lifetime;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        if (ps.main.startLifetime.mode == ParticleSystemCurveMode.Constant)
        {
            lifetime = ps.main.startLifetime.constant;
        } else
        {
            Debug.LogError("Please use constant startlifetime for particle system");
            Destroy(gameObject);
        }

        Destroy(gameObject, lifetime);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
