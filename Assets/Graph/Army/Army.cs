using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Army : MonoBehaviour
{
    Team Team;
    Node Target;
    public int Size { get; private set; }

    public void Send(int size, Node from, Node target)
    {
        Team = from.GetTeam();
        Size = size;
        Target = target;
    }

    public void AttackedBy(int ArmySize)
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
}
