using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorMove : MonoBehaviour
{
    Rigidbody rotRb;
    GameControl control;
    public float rotSpeed = 0;
    void Start()
    {
        rotRb = GetComponent<Rigidbody>();
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

        transform.Rotate(0, 10 * rotSpeed * Time.fixedDeltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            rotRb.AddForce(gameObject.transform.position * 10);
        }
    }
}
