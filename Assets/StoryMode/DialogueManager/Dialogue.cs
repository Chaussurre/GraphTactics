using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : CutsceneEvent
{
    public bool Left;
    [TextArea]
    public string Text;
    public Sprite Character;


    public override void Trigger()
    {
        Manager.ReadDialogue(this);
    }
}
