using System.Collections;
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

    public int recentAction = 3;
    public int chosenAction;
    MachineLearning player;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
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
        // Jump action (click once)
        recentAction = chosenAction;
        //anim.SetBool("sliding", false);

        if (chosenAction == 1 && canJump == true) //jump
        {
            anim.SetBool("sliding", false);
            rb.velocity = Vector2.up * velocity;
        }
        // Slide action (hold)
        else if (chosenAction == 2 || Input.GetMouseButtonDown(0)) //slide
        {
            anim.SetBool("sliding", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("sliding", false);

        }
        else if (chosenAction == 0)
        {
            anim.SetBool("sliding", false);

            //stateController.GameOver();

        }
        else
        {

        }

        if (!stateController.gameOverState) { player.UpdateRewards(); }


        if (rb.velocity.y < 0)
        {
            // Fall animation trigger here
            canJump = false;
        }
        else if (rb.velocity.y > 0)
        {
            // Jump animation trigger here
            canJump = false;
        }
        else
        {
            canJump = true;
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
    }
}
