using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDropArrow : MonoBehaviour
{
    public void SetPosition(Vector2 from, Vector2 to, Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
 
        Quaternion rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(to - from, Vector2.right));
        transform.rotation = rotation;
        transform.position = to;
    }

    public void SetPosition(Vector2 from, NodeSelector to, Color color)
    {
        Vector2 Endpoint = to.GetComponent<Collider2D>().ClosestPoint(from);
        SetPosition(from, Endpoint, color);
    }
}
