using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutsceneEvent : MonoBehaviour
{
    public CutsceneEvent Next;
    protected CutsceneManager Manager;

    private void Awake()
    {
        Manager = GetComponentInParent<CutsceneManager>();
    }

    private void OnDrawGizmos()
    {
        if (Next != null)
            Gizmos.DrawLine(transform.position, Next.transform.position);
    }

    abstract public void Trigger();
}
