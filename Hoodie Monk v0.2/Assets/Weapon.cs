using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject initialFireParticlces;
    private ParticleSystem FiringParticles;

    private void Start(){
        FiringParticles = initialFireParticlces.GetComponent<ParticleSystem>();
    }

    void Update() {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        
    }
    void Shoot()
    {
        FiringParticles.Play();
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
