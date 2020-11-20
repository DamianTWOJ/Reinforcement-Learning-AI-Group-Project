using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCollision : MonoBehaviour
{
    public int score = 0; // Will update when player passes an object
    public Text scoreText; // UI text object
    public GameObject stateControllerObject;
    StateController stateController;

    void Start()
    {
        Debug.Log("Previous Score was: " + StoredData.score); // Output stored previous score for testing
        stateController = stateControllerObject.GetComponent<StateController>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collision Detected");
        if (col.gameObject.tag == "Slide Hazard" || col.gameObject.tag == "Jump Hazard")
        {
            score++;
            scoreText.text = score.ToString(); // Update UI element
        }
    }
}
