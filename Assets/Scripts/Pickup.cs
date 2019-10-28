using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private PickupType pickupType;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private int count = 50;
    [SerializeField] private AudioClip pickupSfx;
    [SerializeField] private float sfxVolume = 0.5f;

    private Ammo _ammo;
    private PlayerHealth _playerHealth;
    private Flashlight _flashlight;
    private AudioSource _audioSource;
    
    enum PickupType
    {
        Ammo,
        Health,
        Battery
    }

    private void Start()
    {
        _ammo = FindObjectOfType<Ammo>();
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _flashlight = FindObjectOfType<Flashlight>();
        _audioSource = FindObjectOfType<PlayerHealth>().GetComponent<AudioSource>();
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
            case PickupType.Battery:
                _flashlight.RechargeFlashlight(count);
                break;
        }
        _audioSource.PlayOneShot(pickupSfx, sfxVolume);
        Destroy(gameObject);
    }
}
