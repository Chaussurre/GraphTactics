using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDropArrow : MonoBehaviour
{
    AutoSender AutoSender = null;
    Node Target;

    public void SetPosition(Vector2 from, Vector2 to, Color color)
    {
        GetComponent<SpriteRenderer>().color = color;
 
        Quaternion rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(to - from, Vector2.right));
        transform.rotation = rotation;
        transform.position = to;
    }

    public void SetPosition(Node from, NodeSelector to, Color color)
    {
        AutoSender = from.AutoSender;
        Target = to.Node;

        Vector2 Endpoint = to.GetComponent<Collider2D>().ClosestPoint(from.transform.position);
        SetPosition(from.transform.position, Endpoint, color);
    }

    private void OnMouseUpAsButton()
    {
        if (AutoSender == null || Target == null)
            return;

        AutoSender.Targets.Remove(Target);
    }
}
