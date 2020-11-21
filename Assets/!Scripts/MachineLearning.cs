/*
Damian Jaundoo | 100623179
Jason Chau | 100618629
Christopher Kompel | 100580618
Shan Rai | 100618348
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineLearning : MonoBehaviour
{

    public StateController stateController;
    public PlayerBehaviour playerBehaviour;
    public ScoreCollision scoreCollision;
    IncomingDetection id;

    public struct Action { public int reward1, reward2, reward3; }
    public static Action action;
    //action 1 jump, action 2 slide, action 3 run

    int tempReward;
    public int tempScore;
    public bool currentGameState;
    public int incomingObstacle;
    public int incomingtotalObstacles;
    public int slideDeathCounter;
    public int jumpDeathCounter;
    public int SetActionRewards(int Actions)
    {
        return Actions;
    }
    public static int GetActionRewards(int Actions)
    {
        return Actions;
    }

    // Use this for initialization // start and update will call themselves through unity 
    void Start()
    {
        playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
        stateController = GameObject.Find("State Controller").GetComponent<StateController>();
        scoreCollision = GameObject.Find("Score Collider").GetComponent<ScoreCollision>();
        id = GameObject.Find("Incoming Object Detection").GetComponent<IncomingDetection>();

        slideDeathCounter = StoredData.slideDeath;
        jumpDeathCounter = StoredData.jumpDeath;
        action.reward3 = Mathf.Max(StoredData.reward2, StoredData.reward1) + 1;
    }
    public int tempState = 0;
    public int prevState = 0;
    public int PerformAction()
    {
        // get the curretn score 
        tempScore = stateController.scoreCollision.score;

        tempState = getState(id.totalSlideObstacle, id.totalJumpObstacle, id.totalObstacle);

        tempReward = MeasureReward();

        prevState = tempState;

        // if reward a > reward b > reward c) do action a 
        currentGameState = stateController.getGameState();
        if (tempReward == 1)
        { // if action results in death or negative occurence
            if (currentGameState == true)
            { // info we need to get, when the action was made and how far the object was 
                return 0;
            }
            else
            {
                // look into how score changed here 
                // increase reward based on how score changed 
                return tempReward;
            }

        }
        else if (tempReward == 2)
        { // action has positive result 
            if (currentGameState == true)
            {
                // player died 
                //decrease this action reward value


                // get game state 
                // modify score 
                // increase rewards slightly for other actions 
                return 0;
            }
            else
            {
                // look into how score changed here 
                // increase reward based on how score changed 
                return tempReward;
            }

        }

        else
        {
            if (currentGameState == true)
            {
                // player died 
                //decrease this action reward value 

                // get game state 
                // modify score 
                // increase rewards slightly for other actions 
                return 0;
            }
            else
            {
                // look into how score changed here 
                // increase reward based on how score changed 
                return 3;
            }

        }

        //reward 1 is jumping , 2 is sliding , 3 is running 
    }
    int MeasureReward()
    {
        //Debug.Log("tempState: " + tempState + "\n prevState: " + prevState);
        // modify the reward values based on states first, this is 3rd level implementation 
        if (prevState != tempState)
        {
            //if (!scoreCollision.collide)
            //{
            if (tempState == 3) //running
            {
                //Debug.Log("Increase RUNNING");
                action.reward3 += (Mathf.Max(jumpDeathCounter, slideDeathCounter));
            }
            else if (tempState == 2) //sliding
            {
                //Debug.Log("Increase SLIDING");
                action.reward2 += ((jumpDeathCounter * jumpDeathCounter) / (id.streak * id.streak));
                //action.reward3--;
            }
            else if (tempState == 1) //jumping
            {
                //Debug.Log("Increase JUMPING");
                action.reward1 += ((slideDeathCounter * slideDeathCounter) / (id.streak * id.streak));
                //action.reward3--;
            }

            //}

            // Debug.Log("Slide Count:" + slideDeathCounter);
            // Debug.Log("Jump Count:" + jumpDeathCounter);
            Debug.Log("Reward 1: " + action.reward1);
            Debug.Log("Reward 2: " + action.reward2);
            Debug.Log("Reward 3: " + action.reward3);
            Debug.Log("--------------------------------");
        }

        if ((action.reward1 > action.reward2) && (action.reward1 > action.reward3))
        {
            return 1;
        }
        if ((action.reward2 > action.reward1) && (action.reward2 > action.reward3))
        {
            return 2;
        }
        if ((action.reward3 >= action.reward1) && (action.reward3 >= action.reward2))
        {
            return 3;
        }
        if (action.reward1 == action.reward2)
        {
            if (tempState == 1)
                return 1;
            else if (tempState == 2)
                return 2;
        }
        // we have to give a reward score 
        // compare currentScore and bestScore 		
        return 0;
    }
    int getState(int slidehazard, int jumphazard, int totalhazard)
    {
        //Debug.Log("totalHazard: " + totalhazard + "\nslideHazard: " + slidehazard + "\njumpHazard: " + jumphazard);
        if (totalhazard == 0) // if no hazard, perform run
        {
            return 3;
        }
        if (slidehazard > jumphazard) //if saw hazard, slide
        {
            return 2;
        }
        if (jumphazard >= slidehazard) //if blocked, jump
        {
            return 1;
        }
        return 0;
    }
    public void UpdateRewards()
    {
        if (stateController.getGameState() == true)
        {
            Debug.Log("Death Action:" + stateController.deathAction);
            Debug.Log("Slide Deaths: " + slideDeathCounter + " | Jump Deaths: " + jumpDeathCounter);
            if (stateController.deathAction == 1)
            {
                //Debug.Log("Decrease JUMP");
                action.reward1 = SetActionRewards(action.reward1 - (slideDeathCounter * slideDeathCounter));
            }
            else if (stateController.deathAction == 2)
            {
                //Debug.Log("Decrease SLIDE");
                action.reward2 = SetActionRewards(action.reward2 - (jumpDeathCounter * jumpDeathCounter));
                //jumpDeathCounter++;

            }
            else if (stateController.deathAction == 3)
            {
                //Debug.Log("Decrease RUN");
                action.reward3 = SetActionRewards(action.reward3 - Mathf.Max(jumpDeathCounter, slideDeathCounter));
            }

        }
        //else if(stateController.getGameState() == false)
        //{	
        //	if(scoreCollision.collide)
        //	{
        //		//Debug.Log("HELLO!");
        //		if(playerBehaviour.recentAction == 3)
        //		{
        //			action.reward3+= Mathf.Max(jumpDeathCounter,slideDeathCounter); 	
        //		}
        //		else if(playerBehaviour.recentAction == 2)
        //		{
        //			action.reward2+= jumpDeathCounter;
        //		}
        //		else if(playerBehaviour.recentAction == 1)
        //		{
        //			action.reward1+=slideDeathCounter; 	
        //		}
        //		scoreCollision.collide = false;
        //	}
        //}
    }
    // Update is called once per frame
    void Update()
    {

    }
}