using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float maxDistance = 3;
    [SerializeField] float offset = 0.85f;

    Transform target;
    bool isFollowing;

    float followingDistance;

    private void Awake()
    {
        followingDistance = offset * maxDistance;
    }
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            float distance = Vector2.Distance(target.position, transform.position);
            float distanceCheck = isFollowing ? followingDistance : maxDistance;
            if (distance >= distanceCheck)
            {
                isFollowing = true;
                Vector3 pos = Vector3.Lerp(transform.position, target.position, moveSpeed);
                pos.z = transform.position.z;

                transform.position = pos;
            }
            else
            {
                isFollowing = false;
            }
        }
    }

    public void ForceUpdate()
    {
        Vector3 pos = target.position;
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
