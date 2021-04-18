using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAutoSendManager : MonoBehaviour
{
    [SerializeField]
    DragNDropArrow ArrowPrefab;
    DragNDropArrow Arrow = null;

    bool AutoSending;

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
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Node ArrowEnd = Graph.Instance.FindClosestNode(MousePos);

        if (!Input.GetMouseButton(0) || ArrowStart == null || ArrowStart.GetComponent<Collider2D>().OverlapPoint(MousePos))
        {
            if (Arrow != null)
                Destroy(Arrow.gameObject);
            Arrow = null;
            AutoSending = false;
            return;
        }

        if (!CanDisplayArrow(ArrowStart))
            return;

        if (Arrow == null)
            Arrow = Instantiate(ArrowPrefab);

        Color color = ArrowStart.Node.GetTeam().GetColor();
        Vector2 start = ArrowStart.transform.position;

        if (ArrowStart.Node.GetEdge(ArrowEnd) != null)
            Arrow.SetPosition(ArrowStart.Node, ArrowEnd.GetComponentInChildren<NodeSelector>(), color);
        else
        {
            color = new Color(color.r, color.g, color.b, 0.5f);
            Arrow.SetPosition(start, Camera.main.ScreenToWorldPoint(Input.mousePosition), color);
        }
    }

    bool CanDisplayArrow(NodeSelector ArrowStart)
    {
        if (ArrowStart == null)
            return false;

        if (!AutoSending)
            return false;

        if (!SelectorManager.Player.Team.Nodes.Contains(ArrowStart.Node))
            return false;

        return true;
    }

    public void StartAutoSending()
    {
        AutoSending = true;
    }

    public bool GetAutoSend(out Node from, out Node target)
    {
        NodeSelector ArrowStart = Graph.Instance.NodeSelectorManager.Selected;
        Node ArrowEnd = Graph.Instance.FindClosestNode(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (Input.GetMouseButtonUp(0) && AutoSending &&
            ArrowStart != null && ArrowStart.Node.GetEdge(ArrowEnd) != null)
        {
            from = ArrowStart.Node;
            target = ArrowEnd;
            Graph.Instance.NodeSelectorManager.UnSelect();
            AutoSending = false;
            return true;
        }

        from = null;
        target = null;
        return false;
    }
}
