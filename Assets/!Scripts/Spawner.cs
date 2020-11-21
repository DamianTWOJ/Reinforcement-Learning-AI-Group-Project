using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 1;
    private float timer = 0;
    public GameObject saw, block;
    public PlayerBehaviour player;
    float randomNum = 0;

    // Update is called once per frame
    void Update()
    {
        // Spawn objects at intervals, reset timer after object is spawned
        if (timer > spawnTime && player.playerDead == false)
        {
            randomNum = Random.Range(0.0f, 1.0f);
           // Debug.Log("ranomdNum = " + randomNum);
            GameObject hazard = null;
            //Instantiate hazardous object.Can be modified to include more hazards.
            if (randomNum < 0.5)
            {
                hazard = Instantiate(saw);
                hazard.transform.position = transform.position + new Vector3(0, -1.3f, 0);
            }
            else if (randomNum > 0.5)
            {
                hazard = Instantiate(block);
                hazard.transform.position = transform.position + new Vector3(0, -2.8f, 0);
                //Debug.Log("X: " + hazard.transform.position.x + " Y: " +
                //hazard.transform.position.y);
            }

            Destroy(hazard, 3); // Delete after exiting screen

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
