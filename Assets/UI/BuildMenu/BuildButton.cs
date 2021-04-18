using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class BuildButton : MonoBehaviour
{
    [SerializeField]
    Building Building = null;
    [SerializeField]
    Image Image = null;
    [SerializeField]
    Text Text = null;
    private void Update()
    {
        if (Text == null || Building == null || Image == null)
            return;
        if (Graph.Instance != null)
        {
            NodeSelector Selected = Graph.Instance.NodeSelectorManager.Selected;
            Team team = Graph.Instance.NodeSelectorManager.Player.Team;
            GetComponentInChildren<Button>().interactable = Selected != null && Selected.Node.CanBuild(Building) && team == Selected.Node.GetTeam();
        }
        Image.sprite = Building.GetIcone();
        Text.text = "Cost : " + Building.GetCost(); 
    }
}
