using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintingWallDestroyScript : MonoBehaviour
{
    GameControl control;
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("lava").GetComponent<GameControl>();
    }

    
    void Update()
    {
        if(control.reStart)
        {
            Destroy(gameObject);
        }
    }
}
