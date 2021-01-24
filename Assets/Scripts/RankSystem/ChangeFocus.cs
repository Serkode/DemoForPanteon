﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFocus : MonoBehaviour
{
    public bool isUsed;
    public PositionManager master;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "opponent" && !isUsed || other.tag == "Player" && !isUsed)
        {
            isUsed = true;
            master.currentPoint++;
        }
    }
}
