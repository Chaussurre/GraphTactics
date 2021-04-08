using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Army))]
public class ArmyMove : MonoBehaviour
{
    Army Army;
    Node From;
    Node Target;

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
    }

    public void Send(int size, Node from, Node target)
    {
        transform.position = from.transform.position;
        From = from;
        Target = target;

        GetComponentInChildren<Text>().text = size.ToString();
        GetComponentInChildren<SpriteRenderer>().color = from.GetTeam().GetColor();

        Army.Send(size, from, target);
    }
}
