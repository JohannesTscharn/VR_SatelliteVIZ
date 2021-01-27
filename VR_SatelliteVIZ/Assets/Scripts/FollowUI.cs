using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowUI : MonoBehaviour
{
    public GameObject UIAnchor;
    public GameObject VRCamera;
    public float speed = 8;

    
    void Update()
    {

        transform.position = Vector3.Lerp(transform.position, UIAnchor.transform.position, speed / 15);

        Quaternion targetRotation = Quaternion.LookRotation(VRCamera.transform.position - transform.position);

        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
    }



}
