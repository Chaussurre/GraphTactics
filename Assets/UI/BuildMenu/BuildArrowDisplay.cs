using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildArrowDisplay : MonoBehaviour
{
    [SerializeField]
    GameObject ArrowPregab;

    GameObject Arrow;

    float timer = 0;

    [SerializeField]
    float Distance;
    [SerializeField]
    float Amplitude;
    [SerializeField, Range(0.01f, 10f)]
    float Speed;

    Button Button;
    private void Start()
    {
        transform.localPosition = Vector3.zero;
        Arrow = Instantiate(ArrowPregab, transform);
        Button = GetComponentInParent<Button>();
        Button.onClick.AddListener(new UnityAction(Destroy));
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        timer += Time.deltaTime;

        Arrow.transform.localPosition = Vector2.right * (Distance + Amplitude * Mathf.Abs(Mathf.Cos(Speed * Mathf.PI * 2 * timer)));
        Arrow.SetActive(Button.interactable);
    }
}
