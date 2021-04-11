using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class CharacterPortrait : MonoBehaviour
{
    void Update()
    {
        Image image = GetComponent<Image>();
        image.enabled = image.sprite != null; 
    }
}
