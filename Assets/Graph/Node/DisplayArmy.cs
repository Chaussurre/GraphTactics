using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways, RequireComponent(typeof(Node))]
public class DisplayArmy : MonoBehaviour
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
            renderer.color = team.GetColor();
        else
            renderer.color = Color.grey;
    }

    void DisplayArmySize(Node node)
    {
        Text text = GetComponentInChildren<Text>();
        if (text == null)
            return;

        text.text = node.GetArmySize().ToString();
    }
}
