using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : Building
{
    [SerializeField]
    CanonRay RayPrefab;

    CanonRay Ray = null;

    [SerializeField, Range(0f, 100f)]
    float DamagePerSec;

    float Damage = 0;

    Army Targetted = null;

    public override void OnUpdate(float deltaTime, Node node)
    {
        ManageTarget(node);
        DealDamage(deltaTime, node);
        UpdateRay(node);
    }

    private void OnDestroy()
    {
        if (Ray != null)
            Destroy(Ray.gameObject);
    }

    void UpdateRay(Node node)
    {
        if (Targetted == null)
            return;

        if(Ray == null)
            Ray = Instantiate(RayPrefab);

        Ray.Target(node, Targetted);
    }

    void DealDamage(float deltaTime, Node node)
    {
        if (Targetted == null)
            return;

        Damage += DamagePerSec * deltaTime;
        if (Damage >= 1)
        {
            int damageDone = Mathf.FloorToInt(Damage);
            Targetted.DealDamage(damageDone);
            Damage -= damageDone;
        }
    }

    void ManageTarget(Node node)
    {
        float rangeSqr = Range * Range;

        if (Targetted != null)
        {
            if (!Targetted.isActiveAndEnabled //Target is dead
                || (node.transform.position - Targetted.transform.position).sqrMagnitude > rangeSqr) //Target is too far
                SetNoTarget();
        }
        else
            SetNoTarget();

        if (Targetted == null)
            Targetted = FindTarget(node.transform.position, rangeSqr, node.GetTeam());
    }

    void SetNoTarget()
    {
        Targetted = null;
        if (Ray != null)
        {
            Destroy(Ray.gameObject);
            Ray = null;
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
