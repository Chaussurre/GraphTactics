using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSender : MonoBehaviour
{
    [SerializeField, Range(0.001f, 3f)]
    float SendRate;

    public Node Node { get; private set; }
    readonly public HashSet<Node> Targets = new HashSet<Node>();

    float timer = 0;

    private void Start()
    {
        Node = GetComponent<Node>();
    }

    void FixedUpdate()
    {
        if (Node == null || Targets == null)
            return;

        if(timer >= SendRate)
        {
            Attack();
            timer -= SendRate;
        }

        timer += Time.fixedDeltaTime;
    }

    void Attack()
    {
        if (Targets.Count == 0)
            return;

        int armySize = Node.GetArmySize() / Targets.Count;
        if (armySize <= 0)
            return;

        foreach (Node target in Targets)
            Node.TryAttack(target, armySize);
    }

    public void TrySetAutoSend(Node target)
    {
        Edge edge = Node.GetEdge(target);
        if (edge == null)
            return;
        
        
        if (Targets.Contains(target))
            Targets.Remove(target); //Setting an auto send a second time cancel the first
        else
        {
            if (target.AutoSender.Targets.Contains(Node))
                target.AutoSender.Targets.Remove(Node); //Setting two auto send in opposite directions cancel both
            else
            {
                Targets.Add(target);
                Node.TryAttack(target, Node.GetArmySize());
            }
        }
    }
}
