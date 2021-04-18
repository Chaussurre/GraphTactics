using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AI that does nothing
public class PassivePlayer : Player
{
    protected override bool Action(out Node from, out Node target, out int Size)
    {
        from = null;
        target = null;
        Size = 0;
        return false;
    }

    protected override bool CreateBuilding(out Node node, out Building buildingPrefab)
    {
        node = null;
        buildingPrefab = null;
        return false;
    }
}
