using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalObstacleMove : MonoBehaviour
{
    Rigidbody rig;
    GameControl control;
    public float obstSpeed = 0;
    void Start()
    {
        rig = GetComponent<Rigidbody>();
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

        transform.position += transform.right * obstSpeed * Time.fixedDeltaTime;

        if(transform.position.x >= 9 || transform.position.x <= -9)
        {
            obstSpeed = obstSpeed * -1;
        }
    }
}
