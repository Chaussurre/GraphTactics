using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Building
{
    [SerializeField, Range(0f, 100f)]
    float DamagePerSec;

    float Damage = 0;
    Army Targetted;

    public override void OnUpdate(float deltaTime, Node node)
    {
        float rangeSqr = Range * Range;
        if (Targetted == null 
            || (node.transform.position - Targetted.transform.position).sqrMagnitude > rangeSqr)
            Targetted = FindTarget(node.transform.position, rangeSqr, node.GetTeam());

        if (Targetted == null)
            return;

        Debug.DrawLine(node.transform.position, Targetted.transform.position);

        Damage += DamagePerSec * deltaTime;
        if (Damage >= 1)
        {
            int damageDone = Mathf.FloorToInt(Damage);
            Targetted.DealDamage(damageDone);
            Damage -= damageDone;
        }
    }

    Army FindTarget(Vector3 position, float rangeSqr, Team team)
    {
        Dictionary<Army, float> distances = new Dictionary<Army, float>();
        foreach (Army army in Graph.Instance.Armies)
        {
            float distance = (position - army.transform.position).sqrMagnitude;

            if (army.Team == team)
                continue;

            if (distance < rangeSqr)
                distances.Add(army, distance);
        }

        float minDist = float.MaxValue;
        Army target = null;
        foreach(KeyValuePair<Army, float> pair in distances)
            if (pair.Value < minDist)
            {
                minDist = pair.Value;
                target = pair.Key;
            }

        return target;
    }
}
