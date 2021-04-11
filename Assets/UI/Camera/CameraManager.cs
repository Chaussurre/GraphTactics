using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Vector3 LastMousePosition;

    Collider2D collider;

    private void Start()
    {
        collider = GetComponentInParent<Collider2D>();
    }

    private void Update()
    {
        Move();
        RecenterCamera();
    }

    void Move()
    {
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
            SetPosition(transform.position + (LastMousePosition - Input.mousePosition) * Time.deltaTime);
        LastMousePosition = Input.mousePosition;
    }

    void RecenterCamera()
    {
        if (!collider.OverlapPoint(transform.position))
            SetPosition(collider.ClosestPoint(transform.position));
    }

    void SetPosition(Vector2 position)
    {
        Vector3 position3 = position;
        transform.position = position3 + Vector3.forward * -10;
    }
}
