using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildingModule))]
public abstract class Building : MonoBehaviour
{
    [SerializeField]
    Sprite Icon;

    abstract public void OnUpdate(float deltaTime, Node node);

    public Sprite GetIcone()
    {
        return Icon;
    }
}
