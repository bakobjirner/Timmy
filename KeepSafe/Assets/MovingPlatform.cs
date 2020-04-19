using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{




    public Transform[] waypoints;
    public float speed;
    private int pointCounter;
    private Vector2 targetPos;
    public float desiredDistance = 0.1f;
    

    // Start is called before the first frame update
    void Start()
    {
       
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
        Vector2 moveDirection = (targetPos - position).normalized * speed*Time.deltaTime;


        if ((targetPos - position).sqrMagnitude < desiredDistance)
        {
            pointCounter++;
            if (pointCounter >= waypoints.Length)
            {
                pointCounter = 0;
            }
            targetPos = waypoints[pointCounter].position;
        }
        
       transform.position = position+moveDirection;

    }
}
