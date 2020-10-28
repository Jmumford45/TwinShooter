using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform FollowTarget;
    public Vector3 TargetOffset;
    public float MoveSpeed = 2f;

    private Transform myTransform;

    private void Start()
    {
        //cache camera Transform 
        myTransform = transform;
    }

    public void SetTarget(Transform aTransform)
    {
        FollowTarget = aTransform;
    }

    private void LateUpdate()
    {
        if (FollowTarget != null)
            myTransform.position = Vector3.Lerp(myTransform.position, FollowTarget.position + TargetOffset, MoveSpeed * Time.deltaTime);
    }
}
