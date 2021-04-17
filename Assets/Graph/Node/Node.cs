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

    public AutoSender AutoSender { get; private set; }

    private void OnEnable() => Team.Nodes.Add(this);
    private void OnDisable() => Team.Nodes.Remove(this);

    private void Start()
    {
        AutoSender = GetComponent<AutoSender>();
        BuildingModule = GetComponentInChildren<BuildingModule>();
    }

    public void AddNeighbourg(Edge edge)
    {
        Neighbourgs.Add(edge);
        edge.GetOtherNode(this).Neighbourgs.Add(edge);
    }

    public void Attacked(Team attacker, int enemyArmy)
    {
        if (Team != attacker)
            ArmySize -= enemyArmy;
        else
        {
            gainArmy(enemyArmy);
            return;
        }

        if (ArmySize < 0)
        {
            AutoSender.Targets.Clear();
            Team.Nodes.Remove(this);
            Team = attacker;
            attacker.Nodes.Add(this);
            ArmySize *= -1;
        }
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

    public bool TryBuild(Building building)
    {
        if (!CanBuild(building))
            return false;
        ArmySize -= building.GetCost();
        BuildingModule.BuildingPrefab = building;
        return true;
    }

    public bool CanBuild(Building building)
    {
        return BuildingModule.BuildingPrefab != building && ArmySize >= building.GetCost();
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
