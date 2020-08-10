using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 shift;
    public float speed;
    private GameObject target;
    private Vector3 targetPosition;

    // Start is calle
    void Start()
    {
        target = GameObject.Find("Cam Target");
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetPosition = target.transform.position+shift ;
        transform.position = Vector3.Slerp(transform.position,targetPosition,speed*Time.deltaTime);
    }
}
