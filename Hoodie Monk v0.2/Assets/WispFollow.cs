using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WispFollow : MonoBehaviour
{

    public float speed;
    private Transform target;
    public float StoppingDistance;



    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {

        if(Vector2.Distance(transform.position, target.position) > StoppingDistance)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
