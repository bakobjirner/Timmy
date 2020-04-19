using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyFly : MonoBehaviour
{

    public Transform[] waypoints;
    public float speed;
    private int pointCounter;
    private Vector2 targetPos;
    public float desiredDistance = 0.1f;
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
        targetPos = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }



    void Move()
    {
        Vector2 position = transform.position;
        Vector2 moveDirection = (targetPos - position).normalized * speed;
        

        if((targetPos - position).sqrMagnitude < desiredDistance)
        {
            pointCounter++;
            if (pointCounter >= waypoints.Length)
            {
                pointCounter = 0;
            }
            targetPos = waypoints[pointCounter].position;
        }
        body.velocity = moveDirection;

    }
}
