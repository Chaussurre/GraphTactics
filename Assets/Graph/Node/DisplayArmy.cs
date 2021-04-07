using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways, RequireComponent(typeof(Node))]
public class DisplayArmy : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Text text = GetComponentInChildren<Text>();
        if (text == null)
            return;

        Node node = GetComponent<Node>();
        text.text = node.GetArmySize().ToString();
    }
}
