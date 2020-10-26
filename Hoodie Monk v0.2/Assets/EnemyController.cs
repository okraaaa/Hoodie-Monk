using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool vertical;
    public float speed = 3.0f;
    public float changeTime = 3.0f;
    public ParticleSystem enemyDeathParticles;
    private ParticleSystem enemyDeathPS;

    public Sprite idleSprite;
    public Sprite hitSprite;

    Rigidbody2D rigidbody2D;
    public int enemyhealth = 15;
    float timer;
    int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }


        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2D.MovePosition(position);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(TakeHit());
        enemyhealth -= damage;
        if (enemyhealth <= 0)
        {
            Die();
        }

    void Die()
        {
            enemyDeathPS = Instantiate(enemyDeathParticles, transform.position, transform.rotation) as ParticleSystem;
            Destroy(enemyDeathPS.gameObject, 3f);
            Destroy(gameObject);
        }
    }

    IEnumerator TakeHit() //Contents not in 'Update()' because it can't be paused
    {
        rigidbody2D.GetComponent<SpriteRenderer>().sprite = hitSprite; //Sets the sprite to the taking damage sprite
        yield return new WaitForSeconds(0.3f); //Pause (makes it look better)
        rigidbody2D.GetComponent<SpriteRenderer>().sprite = idleSprite; //Sets the sprite back to the normal sprite
    }
}
