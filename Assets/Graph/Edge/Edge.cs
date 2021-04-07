using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(SpriteRenderer))]
public class Edge : MonoBehaviour
{
    [SerializeField, Range(0.1f, 0.5f)]
    float size;

    public Node Left;
    public Node Right;

    // Update is called once per frame
    private void Update()
    {
        if (Left == null || Right == null)
            return;
        SetPosition();
        SetColor();
    }

    void SetPosition()
    {
        transform.position = Vector2.Lerp(Left.transform.position, Right.transform.position, 0.5f);

        Vector2 relativPos = Left.transform.position - Right.transform.position;

        transform.localScale = new Vector3(relativPos.magnitude, size, 1);

        float angle = Vector2.SignedAngle(Vector2.left, relativPos);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void SetColor()
    {
        SpriteRenderer LeftRend = Left.GetComponent<SpriteRenderer>();
        SpriteRenderer RightRend = Right.GetComponent<SpriteRenderer>();
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (LeftRend.color == RightRend.color)
            renderer.color = LeftRend.color;
        else
            renderer.color = Color.white;
    }
}
