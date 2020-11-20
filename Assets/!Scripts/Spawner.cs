using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnTime = 1;
    private float timer = 0;
    public GameObject saw, boulder;

    // Update is called once per frame
    void Update()
    {
        // Spawn objects at intervals, reset timer after object is spawned
        if (timer > spawnTime)
        {
            // Instantiate hazardous object. Can be modified to include more hazards.
            GameObject hazard = Instantiate(saw);
            hazard.transform.position = transform.position + new Vector3(0, -1.3f, 0);

            Destroy(hazard, 3); // Delete after exiting screen

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
