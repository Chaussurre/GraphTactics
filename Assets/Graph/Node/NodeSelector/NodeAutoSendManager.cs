using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeAutoSendManager : MonoBehaviour
{
    [SerializeField]
    DragNDropArrow ArrowPrefab;
    DragNDropArrow Arrow = null;

    NodeSelector ArrowStart = null;
    NodeSelector ArrowEnd = null;

    private void Update()
    {
        UpdateArrow();
    }

    void UpdateArrow()
    {
        if (!Input.GetMouseButton(0) || ArrowStart == ArrowEnd)
        {
            if (Arrow != null)
            {
                if (ArrowStart != null && ArrowEnd != null && ArrowStart.Node.GetEdge(ArrowEnd.Node) != null)
                    ArrowStart.Node.TrySetAutoSend(ArrowEnd.Node);
                Destroy(Arrow.gameObject);
            }
            Arrow = null;
            return;
        }

        if (ArrowStart == null)
            return;

        if (Arrow == null)
            Arrow = Instantiate(ArrowPrefab);

        Color color = ArrowStart.Node.GetTeam().GetColor();
        Vector2 start = ArrowStart.transform.position;

        if (ArrowEnd != null && ArrowStart.Node.GetEdge(ArrowEnd.Node) != null)
            Arrow.SetPosition(start, ArrowEnd, color);
        else
            Arrow.SetPosition(start, Camera.main.ScreenToWorldPoint(Input.mousePosition), color);
    }

    public void SetArrowStart(NodeSelector selector)
    {
        ArrowStart = selector;
    }

    public void SetArrowEnd(NodeSelector selector)
    {
        ArrowEnd = selector;
    }
}
