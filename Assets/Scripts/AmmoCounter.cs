using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateCounter(AmmoType type, int count)
    {
        _counter.SetText($"{type}: {count}");
    }
}
