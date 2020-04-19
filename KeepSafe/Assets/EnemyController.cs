using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    Rigidbody2D body;
    public float speed;
    private bool walkRight;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }


    private void Move()
    {
        if (walkRight)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
        }

        bool wallLeft = Physics2D.OverlapCircle(wallCheckLeft.position, groundCheckRadius, groundLayer);
        bool wallRight = Physics2D.OverlapCircle(wallCheckRight.position, groundCheckRadius, groundLayer);

        if (wallLeft &! walkRight)
        {
            walkRight = !walkRight;
        }
        if (wallRight && walkRight)
        {
            walkRight = !walkRight;
        }
    }
}
