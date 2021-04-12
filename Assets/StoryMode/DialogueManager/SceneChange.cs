using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : CutsceneEvent
{
    [SerializeField]
    string Scene;

    public override void Trigger()
    {
        if (Scene.Length > 0)
            SceneManager.LoadScene(Scene);
    }
}
