using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : CutsceneEvent
{
    public override void Trigger()
    {
        Graph.Instance.PauseGame = false;
        Manager.HideDialogue();
    }
}
