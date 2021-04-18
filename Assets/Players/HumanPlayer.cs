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

    protected override bool Action(out Node from, out Node target, out int Size)
    {
        ArmyRationer armyRationer = Graph.Instance.ArmyRationer;
        float ratio = 1f;
        if (armyRationer != null)
            ratio = armyRationer.GetRatio();

        bool result = Graph.Instance.NodeSelectorManager.GetAction(out from, out target);
        if (from != null)
            Size = Mathf.FloorToInt(ratio * from.GetArmySize());
        else
            Size = 0;
        return result;
    }

    protected override bool CreateBuilding(out Node node, out Building buildingPrefab)
    {
        if (BuildMenu == null)
        {
            buildingPrefab = null;
            node = null;
            return false;
        }

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

    protected override bool SetFlux(out Node from, out Node target)
    {
        return Graph.Instance.NodeSelectorManager.AutoSendManager.GetAutoSend(out from, out target);
    }
}
