using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RotateParticle : MonoBehaviour
{

    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        var main = particles.main;
        main.startRotationY = transform.eulerAngles.y * Mathf.Deg2Rad;
    }
}
