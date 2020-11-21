using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectObject : MonoBehaviour
{
    public bool isNear = false;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Jump Hazard" || col.gameObject.tag == "Jump Hazard")
        {
            isNear = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        isNear = false;
    }
}
