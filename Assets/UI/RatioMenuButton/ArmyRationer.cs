using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyRationer : MonoBehaviour
{
    [SerializeField]
    List<float> RatiosList;

    int CurrentRatio = 0;

    Text Text;
    public float GetRatio()
    {
        if (Input.GetAxisRaw("ArmyRatioChange") > 0)
        {
            if (RatiosList[CurrentRatio] == 1f)
                return 0.5f;
            return 1f;
        }
        else
            return RatiosList[CurrentRatio];
    }

    public void ChangeRatio()
    {
        CurrentRatio++;
        CurrentRatio %= RatiosList.Count; 
    }

    private void Start()
    {
        Text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        Text.text = Mathf.FloorToInt(GetRatio() * 100) + "%";
    }
}
