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

    readonly public HashSet<Edge> AutoSend = new HashSet<Edge>();

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
            AutoSend.Clear();
            Team.Nodes.Remove(this);
            Team = attacker;
            attacker.Nodes.Add(this);
            ArmySize *= -1;
        }
    }

    public void TrySetAutoSend(Node node)
    {
        Edge edge = GetEdge(node);
        if (edge != null)
        {
            if (AutoSend.Contains(edge))
                AutoSend.Remove(edge); //Setting an auto send a second time cancel the first
            else
            {
                if (node.AutoSend.Contains(edge))
                    node.AutoSend.Remove(edge); //Setting two auto send in opposite directions cancel both
                else
                {
                    AutoSend.Add(edge);
                    TryAttack(node, GetArmySize());
                }
            }
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
        if (AutoSend.Count > 0)
        {
            int sendArmy = GetArmySize() / AutoSend.Count;
            if (sendArmy >= 1)
                foreach (Edge edge in AutoSend)
                    TryAttack(edge.GetOtherNode(this), sendArmy);
        }
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
