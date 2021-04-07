using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team : MonoBehaviour
{
    [SerializeField]
    Color Color;

    public Color GetColor()
    {
        return Color;
    }
}
