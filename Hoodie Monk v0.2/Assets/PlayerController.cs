using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public float jumpVelocity = 10f;
    private Rigidbody2D rigidbody2d;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    private bool facingRight = true; //REEEEEEEEEEE
    public Transform firePoint;

    public Sprite idleSprite;
    public Sprite jumpSprite;
    public Sprite fallSprite;

    public bool isGrounded;
    public int jumpsLeft;

    public Image background;
    //public float backgroundSpeed;

    public GameObject particleObject;
    public GameObject particleObject2;

    private ParticleSystem landingParticles;
    private ParticleSystem runningParticles;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        landingParticles = particleObject.GetComponent<ParticleSystem>();
        runningParticles = particleObject2.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StopAllCoroutines(); //Stops idle sprite
            animator.enabled = true; //Enables Animation
            transform.position += Vector3.right * speed * Time.deltaTime; //Moves Character
            animator.SetFloat("Move X", lookDirection.x); //Sets direction for animator to move/look in
            //background.transform.position += Vector3.right * backgroundSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StopAllCoroutines();
            animator.enabled = true;
            transform.position += Vector3.left * speed * Time.deltaTime;
            animator.SetFloat("Move X", lookDirection.x - 1);
            if (rigidbody2d.velocity.x < 0.1)
            {
                //background.transform.position += Vector3.left * backgroundSpeed * Time.deltaTime;
            }
        }
        else
        {
            StartCoroutine(Stopped()); //Calls 'IEnumerator Stopped()'
        }
        if (isGrounded == true)
        {
            //jumpsLeft = 1;
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                rigidbody2d.velocity = Vector2.up * jumpVelocity; //Jump
                StopAllCoroutines(); //Stops idle sprite
            }
        }
        //FIX THIS LATER
        if (Input.GetKeyDown(KeyCode.D)){
            if (facingRight != true)
            {
                Debug.Log("aaaaaa");
                firePoint.Rotate(0f, 180f, 0f);
                facingRight = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.A)){
            if (facingRight == true)
            {
                Debug.Log("fuuuuuuu");
                firePoint.Rotate(0f, 180f, 0f);
                facingRight = false;
            }
        }







        //if (isGrounded == true && jumpsLeft == 1)
        //{
        //    if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //        jumpsLeft -= 1;
        //        rigidbody2d.velocity = Vector2.up * jumpVelocity;
        //}
        if (isGrounded == false)
        {
            if (rigidbody2d.velocity.y > 0)
            {
                StopAllCoroutines();
                animator.enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = jumpSprite; //Sets the sprite to jump sprite
                //background.transform.position += Vector3.up * backgroundJumpSpeed * Time.deltaTime;
            }
            else if (rigidbody2d.velocity.y < 0.1)
            {
                StopAllCoroutines();
                animator.enabled = false;
                this.GetComponent<SpriteRenderer>().sprite = fallSprite;
                //background.transform.position += Vector3.down * backgroundJumpSpeed * Time.deltaTime;
            }
            else
            {
                StartCoroutine(Stopped());
            }
        }


        //if (rigidbody2d.velocity.x > 0)
        //{
           // transform.position = new Vector3(transform.position.x + speed, transform.position.y);
            //background.transform.position += new Vector3(1 * Time.deltaTime, 0, 0);
       // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ground"){
                rigidbody2d.GetComponent<PlayerController>().isGrounded = true;
                
        }
        }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground"){
            rigidbody2d.GetComponent<PlayerController>().isGrounded = false;
            //landingParticles.Play();
        }
    }



    IEnumerator Stopped() //Contents not in 'Update()' because it can't be paused
    {
        yield return new WaitForSeconds(0.1f); //Pause (makes it look better)
        this.GetComponent<SpriteRenderer>().sprite = idleSprite; //Sets the sprite to idle sprite
        //runningParticles.Stop(); // Stops running particles
        animator.enabled = false; //Stops Animation
    }
}
