using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField]
    Color Color;

    Player Player;
    readonly public HashSet<Node> Nodes = new HashSet<Node>();

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

    public static Color TeamColor(Team team)
    {
        if (team != null)
            return team.GetColor();
        return Color.grey;
    }
}
