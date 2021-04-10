using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : Building
{
    [SerializeField, Range(0.1f, 10f)]
    float SoldierProduction = 0.1f;

    public override void OnUpdate(float deltaTime, Node node)
    {
        if (node.GetTeam() != null)
            node.gainArmy(SoldierProduction * deltaTime);
    }
}
