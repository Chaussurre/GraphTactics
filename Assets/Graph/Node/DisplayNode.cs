using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways, RequireComponent(typeof(Node))]
public class DisplayNode : MonoBehaviour
{
    // Update is called once per frame

    [SerializeField]
    DragNDropArrow ArrowPrefab;
    Node Node;

    List<DragNDropArrow> arrows = new List<DragNDropArrow>();
    private void Start()
    {
        Node = GetComponent<Node>();
    }

    private void Update()
    {
        DisplayArmySize();
        DisplayTeam();
        DisplayAutoSend();
    }

    void DisplayAutoSend()
    {
        while(arrows.Count < Node.AutoSend.Count)
            arrows.Add(Instantiate(ArrowPrefab, transform.parent));

        while(arrows.Count > Node.AutoSend.Count)
        {
            DragNDropArrow arrow = arrows[arrows.Count - 1];
            Destroy(arrow.gameObject);
            arrows.RemoveAt(arrows.Count - 1);
        }

        int i = 0;
        foreach(Edge edge in Node.AutoSend)
        {
            Color color = Node.GetTeam().GetColor();
            NodeSelector selector = edge.GetOtherNode(Node).GetComponentInChildren<NodeSelector>();
            arrows[i++].SetPosition(transform.position, selector, color);
        }
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
