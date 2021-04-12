using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VicoryDialogue : CutsceneEvent
{
    public override void Trigger()
    {
        GetComponent<Canvas>().enabled = true;
    }
}
