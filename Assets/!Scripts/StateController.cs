using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public GameObject endPanel; // Game over panel
    public ScoreCollision scoreCollision; // Gets score data

    void Start()
    {
        endPanel.SetActive(false); // Hide while game is being played
    }
    public void GameOver()
    {
        StoredData.score = scoreCollision.score; // Storing data before scene reset
        scoreCollision.GetComponent<BoxCollider2D>().enabled = false; // Stop tracking points
        endPanel.SetActive(true); // Show game over screen
    }
}
