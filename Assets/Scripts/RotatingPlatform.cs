using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    GameControl control;
    public float rotSpeed = 0;
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("lava").GetComponent<GameControl>();
    }

    
    void Update()
    {
        if (!control.gameStart)
        {
            return;
        }

        transform.Rotate(0, 0, 10f * rotSpeed * Time.fixedDeltaTime);
    }
}
