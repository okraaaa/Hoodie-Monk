using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public Rigidbody2D rb;
    public ParticleSystem impactEffect;
    public ParticleSystem travelingFire;
    private ParticleSystem boomPs;


    void Start() {
        rb.velocity = transform.right * speed;
    }

    void OnTriggerExit2D(Collider2D hitInfo) // THIS FUNC CALLS TWICE SOMETIMES, CLAIM ITS CRITS FOR NOW, FIX THIS LATER.
    {
        boomPs = Instantiate(impactEffect, transform.position, transform.rotation) as ParticleSystem;
        Debug.Log(hitInfo.name);
        Destroy(boomPs.gameObject, 1f);
        Die();
    }

    public void Die()
    {
        //// This splits the particle off so it doesn't get deleted with the parent
        //travelingFire.transform.SetParent(null);
        rb.GetComponent<Renderer>().enabled = false;
        Destroy(rb);
        travelingFire.Stop();
        Destroy(gameObject, 0.7f);
    }
}