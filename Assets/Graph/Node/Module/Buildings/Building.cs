using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField]
    public float Range;

    [SerializeField, Range(0f, 1f)]
    public float ZoneAlpha;
    
    [SerializeField]
    Sprite Icon;

    abstract public void OnUpdate(float deltaTime, Node node);

    public Sprite GetIcone()
    {
        return Icon;
    }
}
