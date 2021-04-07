using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Army : MonoBehaviour
{
    Team Team;
    int Size;
    Node From;
    Node Target;

    [SerializeField, Range(0.001f, 1f)]
    float Speed;
    float position = 0;


    public void Send(Team team, int size, Node from, Node target)
    {
        Team = team;
        Size = size;
        transform.position = from.transform.position;
        From = from;
        Target = target;

        GetComponentInChildren<Text>().text = size.ToString();
        GetComponentInChildren<SpriteRenderer>().color = team.GetColor();
    }

    private void FixedUpdate()
    {
        if (From == null || Target == null)
            return;
        Vector2 relativPos = Target.transform.position - From.transform.position;
        position += Speed / relativPos.magnitude;


        if(position >= 1)
        {
            Attack();
            return;
        }

        transform.position = Vector2.Lerp(From.transform.position, Target.transform.position, position);
    }

    void Attack()
    {
        Target.Attacked(Team, Size);
        Destroy(gameObject);
    }
}
