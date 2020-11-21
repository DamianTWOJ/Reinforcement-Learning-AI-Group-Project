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

    public static int reward1 = 0;
    public static int reward2 = 0;
    public static int reward3 = 0;

    public static int jumpDeath = 0;
    public static int slideDeath = 0;
    // When writing data to this script in another class, script can simply be reference as follows:
    // StoredData.score = 420;
}
