using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutScript : MonoBehaviour
{
    public float donutRotateSpeed = 0;
    GameControl control;
    void Start()
    {
        control = GameObject.FindGameObjectWithTag("lava").GetComponent<GameControl>();
    }

    
    //void Update()
    //{
        
    //}

    private void FixedUpdate()
    {
        if (!control.gameStart)
        {
            return;
        }
        transform.Rotate(20 * donutRotateSpeed * Time.fixedDeltaTime, 0, 0);
    }
}
