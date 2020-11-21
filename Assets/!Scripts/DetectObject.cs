/*
Damian Jaundoo | 100623179
Jason Chau | 100618629
Christopher Kompel | 100580618
Shan Rai | 100618348
*/
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
