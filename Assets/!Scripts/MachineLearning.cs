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
	public int SetActionRewards(int Actions)
	{
		return Actions; 
	}
	public static int GetActionRewards(int Actions)
	{
		return Actions; 
	}
	

    // Use this for initialization // start and update will call themselves through unity 
    void Start () 
	{
		playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
		stateController = GameObject.Find("State Controller").GetComponent<StateController>();
		scoreCollision = GameObject.Find("Score Collider").GetComponent<ScoreCollision>();
		id = GameObject.Find("Incoming Object Detection").GetComponent<IncomingDetection>();
	}
	public int tempState=0;
	public int prevState=0; 
	public int PerformAction()
	{
		// get the curretn score 
		tempScore = stateController.scoreCollision.score;

		tempState = getState(id.totalSlideObstacle,id.totalJumpObstacle, id.totalObstacle);

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
		// modify the reward values based on states first, this is 3rd level implementation 
		if(prevState!=tempState)
		{
			if(tempState ==3)
			{
				action.reward3 += 2;
			}
			else if(tempState ==2)
			{
				action.reward2 += 2* id.totalSlideObstacle;
			}
			else if(tempState ==1)
			{
				action.reward1 += 2* id.totalSlideObstacle;
			}

		}

		if ((action.reward1 >= action.reward2) && (action.reward1 >= action.reward3))
		{
			return 1;
		}
		if((action.reward2 >= action.reward1) && (action.reward2 >= action.reward3))
		{
			return 2;
		}
		if((action.reward3 >= action.reward1) && (action.reward3 >= action.reward2))
		{
			return 3;
		}
		// we have to give a reward score 
		// compare currentScore and bestScore 		
		return 0; 
	}
	int getState(int slidehazard, int jumphazard, int totalhazard)
	{
		if (totalhazard == 0) // if no hazard, perform run
		{
			return 3;
		}
		if (slidehazard > jumphazard) //if saw hazard, slide
		{
			return 2;
		}
		if (jumphazard > slidehazard) //if blocked, jump
		{
			return 1;
		}
		return 0;
	}
	public void UpdateRewards()
	{
		if(stateController.getGameState() == true)
		{
			if(stateController.deathAction == 1)
			{
						// player died 
				//decrease this action reward value 
				action.reward1 = SetActionRewards(action.reward1-1); 
			}			
			else if(stateController.deathAction == 2)
			{
				action.reward2 = SetActionRewards(action.reward2-1); 
				
			}
			else if(stateController.deathAction == 3)
			{
				action.reward3 = SetActionRewards(action.reward3-1); 
			}
				// get game state 
				// modify score 
				// increase rewards slightly for other actions 
		
		}
		else if(stateController.getGameState() == false)
		{	
			if(scoreCollision.collide)
			{
				//Debug.Log("HELLO!");
				if(playerBehaviour.recentAction == 3)
				{
					action.reward3++; 	
				}
				else if(playerBehaviour.recentAction == 2)
				{
					action.reward2++;
				}
				else if(playerBehaviour.recentAction == 1)
				{
					action.reward1++; 	
				}
				scoreCollision.collide = false;
			}
		}
	}
    // Update is called once per frame
    void Update () {
    
    }
}