using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingDetection : MonoBehaviour
{
    public bool slideObstacle = false;
    public bool jumpObstacle = false;
    public int totalObstacle = 0;
    public int totalJumpObstacle = 0;
    public MachineLearning behaviour;
    public int totalSlideObstacle = 0;
    public string prevTag = ""; // 1 = Jump; 2 = Slide 
    public int streak = 0;

    void Start()
    {
        behaviour = GameObject.Find("RL AI").GetComponent<MachineLearning>();
    }
    public int incomingObstacle(int obstacle)
    {
        return obstacle;
    }
    public int incomingtotalObstacles(int obstacle)
    {
        return obstacle;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!behaviour.playerBehaviour.playerDead)
        {
            if (col.gameObject.tag == "Jump Hazard")
            {
                if (prevTag == col.gameObject.tag) { streak++; } else { streak = 1; }
                prevTag = col.gameObject.tag;
                totalJumpObstacle++;
                behaviour.incomingObstacle = incomingObstacle(1);
                totalObstacle++;
                behaviour.incomingObstacle = incomingtotalObstacles(totalObstacle);
            }
            else if (col.gameObject.tag == "Slide Hazard")
            {
                if (prevTag == col.gameObject.tag) { streak++; } else { streak = 1; }
                prevTag = col.gameObject.tag;
                totalSlideObstacle++;
                behaviour.incomingObstacle = incomingObstacle(2);
                totalObstacle++;
                behaviour.incomingtotalObstacles = incomingtotalObstacles(totalObstacle);

            }
            Debug.Log("prevTag: " + prevTag + " | Streak: " + streak);
        }
    }
}
