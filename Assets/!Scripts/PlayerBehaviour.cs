﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D rb;
    public float velocity = 1;
    public bool playerDead = false;
    public GameObject stateControllerObject;
    StateController stateController;
    bool canJump = true;
    bool isGrounded = false;
    DetectObject incObject;

    public int recentAction = 3;
    public int chosenAction;
    MachineLearning player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        incObject = GetComponentInChildren<DetectObject>();
        stateController = stateControllerObject.GetComponent<StateController>();
        player = GameObject.Find("RL AI").GetComponent<MachineLearning>();
    }

    public void GameOverUpdate()
    {
        player.UpdateRewards();
    }
    // Update is called once per frame
    void Update()
    {
        chosenAction = player.PerformAction();
        Debug.Log("Chosen Action: " + chosenAction);
        // Jump action (click once)
        recentAction = chosenAction;
        //anim.SetBool("sliding", false);

        if (chosenAction == 1 && canJump == true && incObject.isNear == true) //jump
        {
            anim.SetBool("Sliding", false);
            rb.velocity = Vector2.up * velocity;
        }

        else if (chosenAction == 2) //slide
        {
            anim.SetBool("Sliding", true);
        }
        else if (chosenAction == 3)
        {
            anim.SetBool("Sliding", false);
        }
        else if (chosenAction == 0)
        {
            anim.SetBool("Sliding", false);
            //stateController.GameOver();

        }
        else { }
        //}
        //incomingHazard.isNear = false;

        if (!stateController.gameOverState) { player.UpdateRewards(); }


        if (rb.velocity.y < 0)
        {
            // Fall animation trigger here
            //anim.SetBool("Jumping", false);
            //anim.SetBool("Falling", true);
            canJump = false;
        }
        else if (rb.velocity.y > 0)
        {
            // Jump animation trigger here
            //anim.SetBool("Jumping", true);
            //anim.SetBool("Falling", false);
            canJump = false;
        }
        else
        {
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", false);
            canJump = true;
            //isGrounded = true;
        }
    }
    public int getDeathAction()
    {
        return recentAction;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Slide Hazard" || col.gameObject.tag == "Jump Hazard")
        {
            playerDead = true;
            stateController.gameOverState = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            stateController.GameOver();
        }

        if (playerDead)
        {
            if (col.gameObject.tag == "Slide Hazard")
            {
                player.jumpDeathCounter++;
            }
            if (col.gameObject.tag == "Jump Hazard")
            {
                player.slideDeathCounter++;
            }
            StoredData.slideDeath = player.slideDeathCounter;
            StoredData.jumpDeath = player.jumpDeathCounter;
        }
    }
}
