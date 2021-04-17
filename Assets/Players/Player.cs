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
        if (Graph.Instance.PauseGame)
            return;

        {//Does the player wants to attack ?
            if (Action(out Node from, out Node target))
                if (Team.Nodes.Contains(from))
                    from.TryAttack(target, from.GetArmySize());
        }

        {//Does the player wants to create a building ?
            if (CreateBuilding(out Node node, out Building building))
                if (Team.Nodes.Contains(node))
                    node.TryBuild(building);
        }

        {//Does the player wants to set a flow of unit ? HUMAN ONLY
            if (SetFlux(out Node from, out Node target))
                if (Team.Nodes.Contains(from))
                    from.AutoSender.TrySetAutoSend(target);
        }
    }

    protected abstract bool Action(out Node from, out Node target);

    protected abstract bool CreateBuilding(out Node node, out Building buildingPrefab);

    protected virtual bool SetFlux(out Node from, out Node target)
    {
        from = null;
        target = null;
        return false;
    }
}
