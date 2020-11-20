using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoredData : MonoBehaviour
{
    // These were variables that were stored in a main menu scene and then used in the game scene:
    // public static string humanPlayer;
    // public static string aiPlayer;

    // Here's a variable to write the final score to at the end of a run for testing this functionality
    public static int score;


    // When writing data to this script in another class, script can simply be reference as follows:
    // StoredData.score = 420;
}
