using TMPro;
using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCounter(int count)
    {
        _counter.SetText($"Health: {count}");
    }
}
