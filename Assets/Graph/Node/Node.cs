using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField, Range(0f, 100f, order = 0)]
    float ArmySize;

    [SerializeField]
    Team Team;

    public BuildingModule BuildingModule { get; private set; }

    readonly public HashSet<Edge> Neighbourgs = new HashSet<Edge>();

    private void OnEnable() => Team.Nodes.Add(this);
    private void OnDisable() => Team.Nodes.Remove(this);

    private void Start()
    {
        BuildingModule = GetComponentInChildren<BuildingModule>();
    }

    public void AddNeighbourg(Edge edge)
    {
        Neighbourgs.Add(edge);
        edge.GetOtherNode(this).Neighbourgs.Add(edge);
    }


    public void Attacked(Team attacker, int armySize)
    {
        int defendingArmy = GetArmySize();
        int resultArmy = defendingArmy;

        if (Team != attacker)
            resultArmy -= armySize;
        else
            resultArmy += armySize;

        if (resultArmy < 0)
        {
            Team.Nodes.Remove(this);
            Team = attacker;
            Team.Nodes.Add(this);
            resultArmy *= -1;
        }

        this.ArmySize = resultArmy;
    }

    public bool TryAttack(Node target, int ArmySize)
    {
        Edge edge = GetEdge(target);
        if (edge != null)
        {
            edge.SendArmy(this, ArmySize);
            this.ArmySize -= ArmySize;

            return true;
        }
        return false;
    }

    public void gainArmy(float gain)
    {
        ArmySize += gain;
    }

    public int GetArmySize()
    {
        return Mathf.FloorToInt(ArmySize);
    }
    public Team GetTeam()
    {
        return Team;
    }

    public Edge GetEdge(Node other)
    {
        foreach (Edge edge in Neighbourgs)
            if (edge.GetOtherNode(this) == other)
                return edge;
        return null;
    }

    public Edge GetRandomEdge()
    {
        int x = Random.Range(0, Neighbourgs.Count);
        foreach (Edge edge in Neighbourgs)
            if (x-- == 0)
                return edge;
        return null;
    }
}
