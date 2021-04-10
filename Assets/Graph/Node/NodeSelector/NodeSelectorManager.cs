using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelectorManager : MonoBehaviour
{
    NodeSelector Selected = null;
    NodeSelector Targetted = null;

    HumanPlayer player;

    private void Start()
    {
        player = FindObjectOfType<HumanPlayer>();
    }

    public void TrySelect(NodeSelector selector)
    {
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
    
    private void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Selected = null;
        }
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
        Selected = null;
        Targetted = null;
        return true;
    }
}
