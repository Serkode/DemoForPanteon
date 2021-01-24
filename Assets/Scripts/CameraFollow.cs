using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject cam;
    Vector3 Distance;
    void Start()
    {
        Distance =  cam.transform.position - transform.position;
    }

    private void LateUpdate()
    {
        cam.transform.position = transform.position + Distance;
    }
}
