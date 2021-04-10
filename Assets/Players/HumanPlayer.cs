using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanPlayer : Player
{
    NodeSelectorManager SelectorManager;

    protected override void Start()
    {
        base.Start();
        SelectorManager = FindObjectOfType<NodeSelectorManager>();
    }

    protected override bool Action(out Node from, out Node target)
    {
        return SelectorManager.GetAction(out from, out target);
    }
}
