using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways, RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public class NodeSelector : MonoBehaviour
{
    SpriteRenderer Renderer;

    NodeSelectorManager SelectorManager;
    public Node Node { get; private set; }

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Node = GetComponentInParent<Node>();
        SelectorManager = GetComponentInParent<NodeSelectorManager>();
    }

    private void Update()
    {
        if (SelectorManager.IsSelected(this))
            Renderer.color = Color.white;
        else Renderer.color = Color.black;
        Renderer.sprite = Node.GetTeam().GetFaction().NodeShape;
    }

    private void OnMouseUpAsButton()
    {
        SelectorManager.TrySelect(this);
    }
}
