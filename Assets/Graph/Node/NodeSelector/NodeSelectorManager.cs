using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelectorManager : MonoBehaviour
{
    NodeSelector Selected = null;

    public void TrySelect(NodeSelector selector)
    {
        if (Selected == null)
            Selected = selector;
        else
        {
            if (Selected.Node.TryAttack(selector.Node))
                Selected = null;
            else
                Selected = selector;
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Selected = null;
        }
    }

    public bool IsSelected(NodeSelector selector)
    {
        return Selected == selector;
    }
}
