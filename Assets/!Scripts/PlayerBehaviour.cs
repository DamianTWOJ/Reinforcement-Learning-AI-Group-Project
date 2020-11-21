/*
Damian Jaundoo | 100623179
Jason Chau | 100618629
Christopher Kompel | 100580618
Shan Rai | 100618348
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Animator anim;
    private Rigidbody2D rb;
    public float velocity = 1; // Player jump vel
    public bool playerDead = false;
    public GameObject stateControllerObject;
    StateController stateController;
    bool canJump = true;
    bool isGrounded = false;
    DetectObject incObject; // Collider to detect whether player can perform an action

    AudioSource audioSource;

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
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        Debug.Log("--------------------------------");
    }

    public void GameOverUpdate()
    {
        audioSource.Play();
        player.UpdateRewards();
    }

    // Update is called once per frame
    void Update()
    {
        chosenAction = player.PerformAction();
        recentAction = chosenAction;

        // Actions the player can take, choice is decided by the AI
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

        }
        else { anim.SetBool("Sliding", false); }

        if (!stateController.gameOverState) { player.UpdateRewards(); }

        // Animate based on vertical velocity
        if (rb.velocity.y < 0)
        {
            // Fall animation trigger here
            anim.SetBool("Jumping", false);
            anim.SetBool("Falling", true);
            canJump = false;
        }
        else if (rb.velocity.y > 0)
        {
            // Jump animation trigger here
            anim.SetBool("Jumping", true);
            anim.SetBool("Falling", false);
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

    // Check when the player collides with a hazard
    void OnCollisionEnter2D(Collision2D col)
    {
        // If the player collides with a hazard, player death
        if (col.gameObject.tag == "Slide Hazard" || col.gameObject.tag == "Jump Hazard")
        {
            playerDead = true;
            stateController.gameOverState = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            stateController.GameOver();
        }

        // Increase death counters and store data when player dies
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
