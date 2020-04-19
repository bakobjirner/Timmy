using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Bob : MonoBehaviour
{

    private string mode = "walk";
    public float maxHeight;
    Rigidbody2D body;
    public float speed;
    public float jumpForce;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public Transform wallCheckRight;
    Animator animator;
    bool isGrounded;
    public GameObject gameOverScreen;
    public Text scoreText;
    public GameObject startScreen;

    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("space");
        }
    }




    void Move()
    {








        if (body.velocity.x != 0)
        {
            animator.SetBool("isWalking", true);

        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        animator.SetBool("isJumping", !isGrounded);




        switch (mode)
        {

            case "climb":
                climb();
                break;

            default: walk();
                break;


        }
    }




    void walk()
    {
        if (isGrounded)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
        }
        else
        {
            body.velocity = new Vector2(speed*3, body.velocity.y);
        }
        


        //check jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        bool wallRight = Physics2D.OverlapCircle(wallCheckRight.position, groundCheckRadius, groundLayer);


        
        if (isGrounded && body.velocity.y < -maxHeight)
        {
            die("fell to his death");
        }
        
        if (isGrounded && wallRight)
        {
            Jump(1);
        }
        



    }
    void climb()
    {
        body.bodyType = RigidbodyType2D.Kinematic;
        animator.SetBool("isClimbing", true);
        body.velocity = new Vector2(0,speed);
    }
    void stopClimb()
    {
        body.bodyType = RigidbodyType2D.Dynamic;
        animator.SetBool("isClimbing", false);
        Jump(1);
        mode = "walk";
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.tag;
        
        switch (tag)
        {
            case "jump": 
                Jump(1.5f);
                break;
            case "climb":
                mode = "climb";
                break;
            case "stopClimb":
                stopClimb();
                break;
            case "fire":
                die("burned to ash");
                break;
            case "flyingEnemy":
                die("got stabbed to death by a bee");
                break;
            case "ending":
                die("died because I had no time left");
                break;
            default: 
                mode = "walk";
                break;


        }
    }

    private void Jump(float multiplier)
    {
        GetComponentInChildren<AudioSource>().Play();
        body.velocity = new Vector2(body.velocity.x, jumpForce*multiplier);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        



        if (collision.collider.tag.Equals("enemy"))
        {
            die("got eaten by bugs");
        }
        
        

    }



    private void die(string cause)
    {
        Debug.Log("You loose because you couldnt keep Timmy from " + cause);
        
        gameOverScreen.SetActive(true);
        scoreText.text = "Timmy walked " +( Mathf.RoundToInt(transform.position.x)+5) + "m until you failed him and he "+cause ;
        Time.timeScale = 0;
    }

    

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void StartLevel()
    {
        Time.timeScale = 1;
        startScreen.SetActive(false);
    }
    


}
