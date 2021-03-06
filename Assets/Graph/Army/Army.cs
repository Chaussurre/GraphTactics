using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(DisplayArmy))]
public class Army : MonoBehaviour
{
    public Team Team { get; private set; }
    Node Target;
    Edge Edge;
    public int Size { get; private set; }

    private void OnEnable() => Graph.Instance.Armies.Add(this);
    private void OnDisable() => Graph.Instance.Armies.Remove(this);

    public void Send(int size, Node from, Node target, Edge edge)
    {
        Edge = edge;

        if (size == 0)
        {
            Destroy(gameObject);
            return;
        }

        Team = from.GetTeam();
        Size = size;
        Target = target;
        GetComponent<DisplayArmy>().Send(from, target);
    }

    public void DealDamage(int ArmySize)
    {
        Size -= ArmySize;
        if (Size <= 0)
            Destroy(gameObject);
    }

    public void Attack()
    {
        Target.Attacked(Team, Size);
        Destroy(gameObject);
    }

    public bool TryCollide(Army other)
    {
        DisplayArmy armyMove = GetComponent<DisplayArmy>();
        DisplayArmy otherMove = other.GetComponent<DisplayArmy>();

        if (armyMove.isColliding(otherMove))
        {
            int s = Size;
            DealDamage(other.Size);
            other.DealDamage(s);
            return true;
        }

        return false;
    }

    private void OnDestroy()
    {
        Edge.Armies.Remove(this);
    }
}
