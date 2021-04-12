using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField]
    CutsceneEvent FirstEvent;
    [SerializeField]
    CutsceneEvent WinEvent;

    [SerializeField]
    Text TextDisplay;

    [SerializeField]
    Image LeftCharacter;
    [SerializeField]
    Image RightCharacter;

    void SetCharacterTalking(Sprite sprite, bool left)
    {
        if(left)
        {
            LeftCharacter.color = Color.white;
            RightCharacter.color = Color.grey;
            LeftCharacter.sprite = sprite;
        }
        else
        {
            LeftCharacter.color = Color.grey;
            RightCharacter.color = Color.white;
            RightCharacter.sprite = sprite;
        }
    }

    private void Start()
    {
        TriggerCutscene();
    }

    public void ReadDialogue(Dialogue dialogue)
    {
        TextDisplay.transform.parent.gameObject.SetActive(true);
        TextDisplay.text = dialogue.Text;
        SetCharacterTalking(dialogue.Character, dialogue.Left);
    }

    public void HideDialogue()
    {
        TextDisplay.transform.parent.gameObject.SetActive(false);
    }
    public void TriggerCutscene()
    {
        if (FirstEvent == null)
            return;

        FirstEvent.Trigger();
        FirstEvent = FirstEvent.Next;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            TriggerCutscene();

        if(Graph.Instance != null && Graph.Instance.GetWinner() != null)
        {
            FirstEvent = WinEvent;
            TriggerCutscene();
        }
    }

    private void OnDrawGizmos()
    {
        if (FirstEvent != null)
            Gizmos.DrawLine(Vector3.zero, FirstEvent.transform.position);

        if (WinEvent != null)
            Gizmos.DrawLine(Vector3.zero, WinEvent.transform.position);
    }
}
