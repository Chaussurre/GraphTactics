using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    public bool BuildingsStopped = false;
    [SerializeField]
    Color Color;

    Player Player;
    readonly public HashSet<Node> Nodes = new HashSet<Node>();

    [SerializeField]
    Faction Faction;

    private void Start()
    {
        Player = GetComponent<Player>();
        if (Player == null)
            Debug.LogError("No player attached to Team " + this);
    }

    public Color GetColor()
    {
        return Color;
    }

    public Faction GetFaction()
    {
        return Faction;
    }

    public static Color TeamColor(Team team)
    {
        if (team != null)
            return team.GetColor();
        return Color.grey;
    }
}
