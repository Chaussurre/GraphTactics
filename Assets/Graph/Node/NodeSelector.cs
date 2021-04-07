using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public class NodeSelector : MonoBehaviour
{
    SpriteRenderer renderer;

    static NodeSelector Selected = null;
    public Node Node { get; private set; }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Node = GetComponentInParent<Node>();
    }

    private void Update()
    {
        renderer.enabled = Selected == this;
    }

    private void OnMouseUpAsButton()
    {
        if (Selected == null)
            Selected = this;
        else
        {
            Selected.Node.TryAttack(Node);
            Selected = null;
        }
    }
}
