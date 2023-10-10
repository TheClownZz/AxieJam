using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum Axis
{
    X,
    Y,
    Z
}

public class RotateByDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Axis axis;
    public float rotationSpeed;
    public float rotationDamping;

    private float _rotationVelocity;
    private bool _dragged;
    private Vector3 rotationAxis;


    private Vector3 GetAxisRotation()
    {
        switch (axis)
        {
            case Axis.X:
                return Vector3.right;

            case Axis.Y:
                return Vector3.up;

            case Axis.Z:
                return Vector3.back;

            default:
                return Vector3.up;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rotationAxis = GetAxisRotation();
        _dragged = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rotationVelocity = eventData.delta.x * rotationSpeed;
        transform.Rotate(rotationAxis, -_rotationVelocity, Space.Self);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _dragged = false;
    }

    private void Update()
    {
        if (!_dragged && !Mathf.Approximately(_rotationVelocity, 0))
        {
            float deltaVelocity = Mathf.Min(
                Mathf.Sign(_rotationVelocity) * Time.deltaTime * rotationDamping,
                Mathf.Sign(_rotationVelocity) * _rotationVelocity
            );
            _rotationVelocity -= deltaVelocity;
            transform.Rotate(rotationAxis, -_rotationVelocity, Space.Self);
        }
    }

}
