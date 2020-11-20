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
        if(!behaviour.playerBehaviour.playerDead)
        {
            if (col.gameObject.tag == "Jump Hazard")
            {
                totalJumpObstacle++;
                behaviour.incomingObstacle = incomingObstacle(1);
                totalObstacle++;
                behaviour.incomingObstacle = incomingtotalObstacles(totalObstacle);
            }
            else if (col.gameObject.tag == "Slide Hazard")
            {

                totalSlideObstacle++;
                behaviour.incomingObstacle = incomingObstacle(2);
                totalObstacle++;
                behaviour.incomingtotalObstacles = incomingtotalObstacles(totalObstacle);

            }
        }
    }
}
