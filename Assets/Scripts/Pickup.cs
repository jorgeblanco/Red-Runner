using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int count = 50;

    private Ammo _ammo;
    private PlayerHealth _playerHealth;
    
    enum PickupType
    {
        Ammo,
        Health
    }

    private void Start()
    {
        _ammo = FindObjectOfType<Ammo>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>() == null) {return;}

        HandlePickup();
    }

    private void HandlePickup()
    {
        switch (pickupType)
        {
            case PickupType.Ammo:
                _ammo.AddAmmo(ammoType, count);
                break;
            case PickupType.Health:
                _playerHealth.TakeDamage(-count);
                break;
        }
        Debug.Log($"Picked up {count} {pickupType}");
        Destroy(gameObject);
    }
}
