using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float maxDistance = 3;
    [SerializeField] float offset = 0.85f;

    [Space(10)]
    [SerializeField] float top = 8.5f;
    [SerializeField] float bot = -5.5f;
    [SerializeField] float left = -9f;
    [SerializeField] float right = 9.5f;

    Transform target;
    bool isFollowing;

    float followingDistance;

    private void Awake()
    {
        followingDistance = offset * maxDistance;
        SetTarget(GameManager.Instance.player.transform);
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
               // pos.x = Mathf.Clamp(pos.x, left, right);
               // pos.y = Mathf.Clamp(pos.y, bot, top);

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
