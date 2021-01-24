using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingStick : MonoBehaviour
{
    public float hit = 0;
    Rigidbody rigb;
    Vector3 loc;
    GameControl control;
    bool hitPlayer = false, thrown = false;
    float thrownTime = 0;
    void Start()
    {
        rigb = GetComponent<Rigidbody>();
        loc = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        control = GameObject.FindGameObjectWithTag("lava").GetComponent<GameControl>();
    }


    private void FixedUpdate()
    {
        if (!control.gameStart)
        {
            return;
        }

        if (hitPlayer)
        {
            transform.position += transform.right * hit * Time.fixedDeltaTime;
        }

        if(thrown)
        {
            thrownTime += Time.deltaTime;
            if(thrownTime >= 2)
            {
                transform.position = loc;
                thrownTime = 0;
                thrown = false;
                hitPlayer = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "opponent")
        {
            hitPlayer = true;
            thrown = true;
        }
    }

}
