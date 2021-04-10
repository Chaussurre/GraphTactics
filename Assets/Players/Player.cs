using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Team))]
public abstract class Player : MonoBehaviour
{
    public Team Team { get; private set; }

    protected virtual void Start()
    {
        Team = GetComponent<Team>();
    }

    private void Update()
    {
        if (Action(out Node from, out Node Target))
            if (Team.Nodes.Contains(from))
                from.TryAttack(Target, from.GetArmySize());
    }

    protected abstract bool Action(out Node from, out Node target);
}
