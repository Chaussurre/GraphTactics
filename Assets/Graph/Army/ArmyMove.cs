using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmyMove : MonoBehaviour
{
    public Node From { get; private set; }
    public Node Target { get; private set; }
    Army Army;

    [SerializeField, Range(0.001f, 1f)]
    float Speed;

    public float position { get; private set; } = 0;

    private void Awake()
    {
        Army = GetComponent<Army>();
    }

    private void FixedUpdate()
    {
        if (From == null || Target == null)
            return;
        Vector2 relativPos = Target.transform.position - From.transform.position;
        position += Speed / relativPos.magnitude;


        if (position >= 1)
        {
            Army.Attack();
            return;
        }

        transform.position = Vector2.Lerp(From.transform.position, Target.transform.position, position);
        GetComponentInChildren<Text>().text = Army.Size.ToString();
    }

    public void Send(Node from, Node target)
    {
        transform.position = from.transform.position;
        From = from;
        Target = target;

        GetComponentInChildren<SpriteRenderer>().color = from.GetTeam().GetColor();
    }

    public bool isColliding(ArmyMove other) //Only call on two armies on the same edge
    {
        if (Army.Team == other.Army.Team) //Same team
            return false;

        if (other.From == From) //Same direction
            return false;

        if (position < 1 - other.position) //Have yet to meet
            return false;

        return true;
    }
}
