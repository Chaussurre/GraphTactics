using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonRay : MonoBehaviour
{
    [SerializeField, Range(0.001f, .5f)]
    float RaySize;

    Node Node;

    [SerializeField]
    GameObject Ray;

    Army Targetted;
    
    public void Target(Node from, Army army)
    {
        Targetted = army;
        Node = from;
    }

    private void Update()
    {
        if (Targetted == null || Node == null)
            return;
     
        transform.position = Targetted.transform.position;
        Vector2 RelativPos = Targetted.transform.position - Node.transform.position;
        float angle = Vector2.SignedAngle(RelativPos, Vector2.right);

        Ray.transform.position = Vector2.Lerp(Node.transform.position, Targetted.transform.position, 0.5f);
        Ray.transform.rotation = Quaternion.Euler(0, 0, -angle);
        Ray.transform.localScale = new Vector3(RelativPos.magnitude, RaySize, 1);
    }
}
