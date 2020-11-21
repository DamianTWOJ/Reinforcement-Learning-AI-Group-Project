using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingObject : MonoBehaviour
{
    public bool isNear = false;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "jumpHazard" || col.gameObject.tag == "slideHazard")
            isNear = true;
        else
            isNear = false;
    }
}
