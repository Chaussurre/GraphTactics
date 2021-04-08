using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(SpriteRenderer), typeof(Edge))]
public class EdgePosition : MonoBehaviour
{
    [SerializeField, Range(0.1f, 0.5f)]
    float size;

    // Update is called once per frame
    private void Update()
    {
        Edge edge = GetComponent<Edge>();
        if (edge.Left == null || edge.Right == null)
            return;
        SetPosition(edge);
        SetColor(edge);
    }

    void SetPosition(Edge edge)
    {
        transform.position = Vector2.Lerp(edge.Left.transform.position, edge.Right.transform.position, 0.5f);

        Vector2 relativPos = edge.Left.transform.position - edge.Right.transform.position;

        transform.localScale = new Vector3(relativPos.magnitude, size, 1);

        float angle = Vector2.SignedAngle(Vector2.left, relativPos);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void SetColor(Edge edge)
    {
        SpriteRenderer LeftRend = edge.Left.GetComponent<SpriteRenderer>();
        SpriteRenderer RightRend = edge.Right.GetComponent<SpriteRenderer>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (LeftRend.color == RightRend.color)
            renderer.color = LeftRend.color;
        else
            renderer.color = Color.white;
    }
}
