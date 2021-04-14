using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelectorManager : MonoBehaviour
{
    public NodeSelector Selected { get; private set; } = null;
    NodeSelector Targetted = null;
    [HideInInspector]
    public NodeAutoSendManager AutoSendManager;

    HumanPlayer player;

    private void Start()
    {
        player = FindObjectOfType<HumanPlayer>();
        AutoSendManager = GetComponent<NodeAutoSendManager>();
    }

    public void TrySelect(NodeSelector selector)
    {
        if (Graph.Instance.PauseGame)
            return;

        if (Selected == null)
            Selected = selector;
        else
        {
            if (Selected.Node.GetEdge(selector.Node) 
                && player.Team.Nodes.Contains(Selected.Node))
                Targetted = selector;
            else
                Selected = selector;
        }
    }

    public void UnSelect()
    {
        Targetted = null;
        Selected = null;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(1))
            UnSelect();
    }

    public bool IsSelected(NodeSelector selector)
    {
        return Selected == selector;
    }

    public bool GetAction(out Node From, out Node Target)
    {
        From = null;
        Target = null;

        if (Selected == null || Targetted == null)
            return false;
     
        From = Selected.Node;
        Target = Targetted.Node;
        UnSelect();
        return true;
    }
}
