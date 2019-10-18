using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float range = 50f;
    [SerializeField] private int gunDamage = 10;
    [SerializeField] private int shotsPerSecond = 10;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitFxPrefab;
    
    private bool _isFiring;
    private Ammo _ammo;
    private double _timeToNextShot;

    private void Start()
    {
        _ammo = FindObjectOfType<Ammo>();
    }

    private void Update()
    {
        if (_isFiring)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        GetInput();
    }

    private void GetInput()
    {
        _isFiring = Input.GetButton("Fire1");
    }

    private void Shoot()
    {
        if(Time.time < _timeToNextShot || _ammo.GetAmmoCount() <= 0) {return;}
        
        PlayShootFx();
        ProcessRaycast();
        _ammo.AddAmmo(-1);
        _timeToNextShot = Time.time + (1f / shotsPerSecond);
    }

    private void PlayShootFx()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        var fpCameraTransform = fpCamera.transform;
        if (!Physics.Raycast(fpCameraTransform.position, fpCameraTransform.forward, out var hit, range)) return;

        PlayHitFx(hit);
        var damageable = hit.transform.GetComponent<IDamageable>();
        damageable?.TakeDamage(gunDamage);
    }

    private void PlayHitFx(RaycastHit hit)
    {
        var hitFx = Instantiate(hitFxPrefab, hit.point, Quaternion.identity, transform);
        hitFx.transform.forward = hit.normal;
    }
}
