using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    private Ammo _ammo;
    private TextMeshProUGUI _counter;

    private void Start()
    {
        _ammo = FindObjectOfType<Ammo>();
        _counter = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        _counter.SetText($"Ammo: {_ammo.GetAmmoCount()}");
    }
}
