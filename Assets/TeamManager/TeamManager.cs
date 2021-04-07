using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    readonly public List<Team> Teams = new List<Team>();
    // Start is called before the first frame update
    void Start()
    {
        Teams.AddRange(GetComponentsInChildren<Team>());
    }
}
