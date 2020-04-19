using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameButton : MonoBehaviour
{

    public GameObject toSpawn;
    public Transform spawn;
    private bool used;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {



        GetComponent<AudioSource>().Play();
        if (!used)
        {
            used = true;
            Instantiate(toSpawn, spawn.position, Quaternion.identity);
        }
        animator.SetBool("isPressed", true);
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("isPressed", false);
    }


}
