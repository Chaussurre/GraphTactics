using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    int armySize;

    [SerializeField]
    Team Team;

    public int GetArmySize()
    {
        return armySize;
    }

    public Team GetTeam()
    {
        return Team;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
