using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAutoSendManager : MonoBehaviour
{
    [SerializeField]
    DragNDropArrow ArrowPrefab;
    DragNDropArrow Arrow = null;

    NodeSelector ArrowEnd = null;
    bool Autosending;

    NodeSelectorManager SelectorManager;

    private void Start()
    {
        SelectorManager = GetComponent<NodeSelectorManager>();
    }

    private void Update()
    {
        UpdateArrow();
    }

    void UpdateArrow()
    {
        NodeSelector ArrowStart = Graph.Instance.NodeSelectorManager.Selected;

        if (!Input.GetMouseButton(0) || ArrowStart == ArrowEnd)
        {
            if (Arrow != null)
                Destroy(Arrow.gameObject);
            Arrow = null;
            Autosending = false;
            return;
        }

        if (!CanDisplayArrow(ArrowStart))
            return;

        if (Arrow == null)
            Arrow = Instantiate(ArrowPrefab);

        Color color = ArrowStart.Node.GetTeam().GetColor();
        Vector2 start = ArrowStart.transform.position;

        if (ArrowEnd != null && ArrowStart.Node.GetEdge(ArrowEnd.Node) != null)
            Arrow.SetPosition(ArrowStart.Node, ArrowEnd, color);
        else
            Arrow.SetPosition(start, Camera.main.ScreenToWorldPoint(Input.mousePosition), color);
    }

    bool CanDisplayArrow(NodeSelector ArrowStart)
    {
        if (ArrowStart == null)
            return false;

        if (!Autosending)
            return false;

        if (!SelectorManager.Player.Team.Nodes.Contains(ArrowStart.Node))
            return false;

        return true;
    }

    public void StartAutoSending()
    {
        Autosending = true;
    }

    public void SetArrowEnd(NodeSelector selector)
    {
        ArrowEnd = selector;
    }

    public bool GetAutoSend(out Node from, out Node target)
    {
        NodeSelector ArrowStart = Graph.Instance.NodeSelectorManager.Selected;
        if (!Input.GetMouseButton(0) &&
            ArrowStart != null && ArrowEnd != null &&
            ArrowStart.Node.GetEdge(ArrowEnd.Node) != null)
        {
            from = ArrowStart.Node;
            target = ArrowEnd.Node;
            Graph.Instance.NodeSelectorManager.UnSelect();
            ArrowEnd = null;
            return true;
        }

        from = null;
        target = null;
        return false;
    }
}
