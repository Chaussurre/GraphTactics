using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways, RequireComponent(typeof(Node))]
public class DisplayNode : MonoBehaviour
{
    // Update is called once per frame

    Node Node;

    private void Start()
    {
        Node = GetComponent<Node>();
    }

    private void Update()
    {
        DisplayArmySize();
        DisplayTeam();
    }

    void DisplayTeam()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Team team = Node.GetTeam();
        if (team != null)
        {
            renderer.color = team.GetColor();
            renderer.sprite = team.GetFaction().NodeShape;
        }
        else
            renderer.color = Color.grey;
    }

    void DisplayArmySize()
    {
        Text text = GetComponentInChildren<Text>();
        if (text == null)
            return;

        text.text = GetString(Node.GetArmySize());
    }

    public static string GetString(int ArmySize)
    {
        if (ArmySize < 1000)
            return ArmySize.ToString();
        else
        {
            int left = ArmySize / 1000;
            int right = (ArmySize % 1000) / 100;
            return left.ToString() + "K" + right;
        }
    }
}
