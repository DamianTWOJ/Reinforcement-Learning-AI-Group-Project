/*
Damian Jaundoo | 100623179
Jason Chau | 100618629
Christopher Kompel | 100580618
Shan Rai | 100618348
*/
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
    IncomingDetection id;

    public bool collide = false;

    void Start()
    {
        //Debug.Log("Previous Score was: " + StoredData.score); // Output stored previous score for testing
        stateController = stateControllerObject.GetComponent<StateController>();
        id = GameObject.Find("Incoming Object Detection").GetComponent<IncomingDetection>();
    }

    // Update score based on detection
    void OnCollisionEnter2D(Collision2D col)
    {
        collide = true;
        if (col.gameObject.tag == "Slide Hazard" || col.gameObject.tag == "Jump Hazard")
        {
            score++;
            scoreText.text = score.ToString(); // Update UI element
        }

        if (col.gameObject.tag == "Jump Hazard")
        {
            id.totalJumpObstacle--;
            id.totalObstacle--;
        }
        else if (col.gameObject.tag == "Slide Hazard")
        {
            id.totalSlideObstacle--;
            id.totalObstacle--;
        }
    }
}
