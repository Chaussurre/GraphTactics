using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public class NodeSelector : MonoBehaviour
{
    SpriteRenderer renderer;

    NodeSelectorManager SelectorManager;
    public Node Node { get; private set; }

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        Node = GetComponentInParent<Node>();
        SelectorManager = GetComponentInParent<NodeSelectorManager>();
    }

    private void Update()
    {
        renderer.enabled = SelectorManager.IsSelected(this);
    }

    private void OnMouseUpAsButton()
    {
        SelectorManager.TrySelect(this);
    }
}
