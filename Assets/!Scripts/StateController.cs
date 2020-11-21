using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StateController : MonoBehaviour
{
    public GameObject endPanel; // Game over panel
    public ScoreCollision scoreCollision; // Gets score data
    public bool gameOverState;
    public PlayerBehaviour playerData;
    public int deathAction;

    void Start()
    {
        gameOverState = false;
        endPanel.SetActive(false); // Hide while game is being played
        playerData = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        scoreCollision = GameObject.Find("Score Collider").GetComponent<ScoreCollision>();
    }
    public bool getGameState()
    {
        return gameOverState;
    }
    public void GameOver()
    {
        deathAction = playerData.getDeathAction();

        gameOverState = true;
        playerData.GameOverUpdate();
        StoredData.score = scoreCollision.score; // Storing data before scene reset
        StoredData.reward1 = MachineLearning.action.reward1;
        StoredData.reward2 = MachineLearning.action.reward2;
        StoredData.reward3 = MachineLearning.action.reward3;
        //Debug.Log("Stored Data Reward 1: " + StoredData.reward1);
        //Debug.Log("Stored Data Reward 2: " + StoredData.reward2);
        //Debug.Log("Stored Data Reward 3: " + StoredData.reward3);


        scoreCollision.GetComponent<BoxCollider2D>().enabled = false; // Stop tracking points
        endPanel.SetActive(true); // Show game over screen
    }
}
