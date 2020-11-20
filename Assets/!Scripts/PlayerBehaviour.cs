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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        stateController = stateControllerObject.GetComponent<StateController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jump action (click once)
        if (Input.GetMouseButtonDown(0) && canJump == true)
        {
            rb.velocity = Vector2.up * velocity;
        }
        // Slide action (hold)
        else if (Input.GetMouseButton(1))
        {
            anim.SetBool("sliding", true);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("sliding", false);
        }

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Slide Hazard" || col.gameObject.tag == "Jump Hazard")
        {
            playerDead = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            stateController.GameOver();
        }
    }
}
