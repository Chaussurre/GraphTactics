﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ArmyMove))]
public class Army : MonoBehaviour
{
    public Team Team { get; private set; }
    Node Target;
    Edge Edge;
    public int Size { get; private set; }

    public void Send(int size, Node from, Node target, Edge edge)
    {
        Team = from.GetTeam();
        Size = size;
        Target = target;
        Edge = edge;
        GetComponent<ArmyMove>().Send(from, target);
    }

    void AttackedBy(int ArmySize)
    {
        Size -= ArmySize;
        if (Size < 0)
            Destroy(gameObject);
    }

    public void Attack()
    {
        Target.Attacked(Team, Size);
        Destroy(gameObject);
    }

    public bool TryCollide(Army other)
    {
        ArmyMove armyMove = GetComponent<ArmyMove>();
        ArmyMove otherMove = other.GetComponent<ArmyMove>();

        if (armyMove.isColliding(otherMove))
        {
            int s = Size;
            AttackedBy(other.Size);
            other.AttackedBy(s);
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        Edge.Armies.Remove(this);
    }
}