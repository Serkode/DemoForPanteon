using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingStick : MonoBehaviour
{
    Rigidbody rigidStick;
    Vector3 vec;
    void Start()
    {
        rigidStick = GetComponent<Rigidbody>();
    }


    //void Update()
    //{

    //}

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            vec = collision.transform.position;
            //collision.rigidbody.AddForceAtPosition(new Vector3(rigidStick.angularVelocity.x, rigidStick.angularVelocity.y, rigidStick.angularVelocity.z), vec);
            collision.rigidbody.AddRelativeForce(vec * -2 , ForceMode.Force);
        }
    }
}
