using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Team))]
public abstract class Player : MonoBehaviour
{
    public Team Team { get; private set; }

    protected virtual void Awake()
    {
        Team = GetComponent<Team>();
    }

    private void Update()
    {
        if (Action(out Node from, out Node Target))
            if (Team.Nodes.Contains(from))
                from.TryAttack(Target, from.GetArmySize());

        if (CreateBuilding(out Node node, out Building building))
            node.BuildingModule.BuildingPrefab = building;
    }

    protected abstract bool Action(out Node from, out Node target);

    protected abstract bool CreateBuilding(out Node node, out Building buildingPrefab);
}
