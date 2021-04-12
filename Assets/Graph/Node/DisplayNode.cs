using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways, RequireComponent(typeof(Node))]
public class DisplayNode : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        Node node = GetComponent<Node>();
        DisplayArmySize(node);
        DisplayTeam(node);
    }

    void DisplayTeam(Node node)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        Team team = node.GetTeam();
        if (team != null)
        {
            renderer.color = team.GetColor();
            renderer.sprite = team.GetFaction().NodeShape;
        }
        else
            renderer.color = Color.grey;
    }

    void DisplayArmySize(Node node)
    {
        Text text = GetComponentInChildren<Text>();
        if (text == null)
            return;

        text.text = GetString(node.GetArmySize());
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
