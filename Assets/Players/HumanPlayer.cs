using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    BuildMenu BuildMenu;
    protected override void Awake()
    {
        base.Awake();
        BuildMenu = FindObjectOfType<BuildMenu>();
    }

    protected override bool Action(out Node from, out Node target)
    {
        return Graph.Instance.NodeSelectorManager.GetAction(out from, out target);
    }

    protected override bool CreateBuilding(out Node node, out Building buildingPrefab)
    {
        NodeSelectorManager SelectorManager = Graph.Instance.NodeSelectorManager;
        if (BuildMenu.GetCreatedBuilding(out buildingPrefab) && SelectorManager.Selected != null)
        {
            node = SelectorManager.Selected.Node;
            SelectorManager.UnSelect();
            return true;
        }
        node = null;
        return false;
    }
}
