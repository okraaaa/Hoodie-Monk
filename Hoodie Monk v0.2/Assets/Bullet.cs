using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public ParticleSystem impactEffect;
    public ParticleSystem travelingFire;


    void Start() {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Debug.Log(hitInfo.name);
        //Destroy(impactEffect.gameObject, 1f);
        Die();
    }

    public void Die() // Call this immediately before you destroy your missile
    {
        //// This splits the particle off so it doesn't get deleted with the parent
        //travelingFire.transform.SetParent(null);

        Destroy(rb);
        travelingFire.Stop();
        rb.GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 0.7f);
    }
}