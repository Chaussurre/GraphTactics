using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AutoSender))]
public class DisplayAutoSender : MonoBehaviour
{
    [SerializeField]
    DragNDropArrow ArrowPrefab;

    AutoSender AutoSender;
    
    List<DragNDropArrow> arrows = new List<DragNDropArrow>();

    private void Start()
    {
        AutoSender = GetComponent<AutoSender>();
    }

    private void Update()
    {
        DisplayAutoSend();
    }

    void DisplayAutoSend()
    {
        while (arrows.Count < AutoSender.Targets.Count)
            arrows.Add(Instantiate(ArrowPrefab, transform.parent));

        while (arrows.Count > AutoSender.Targets.Count)
        {
            DragNDropArrow arrow = arrows[arrows.Count - 1];
            Destroy(arrow.gameObject);
            arrows.RemoveAt(arrows.Count - 1);
        }

        int i = 0;
        foreach (Node target in AutoSender.Targets)
        {
            Color color = AutoSender.Node.GetTeam().GetColor();
            NodeSelector selector = target.GetComponentInChildren<NodeSelector>();
            arrows[i++].SetPosition(AutoSender.Node, selector, color);
        }
    }

}
