﻿using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(AudioSource))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float range = 50f;
    [SerializeField] private int gunDamage = 10;
    [SerializeField] private int shotsPerSecond = 10;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitFxPrefab;
    [SerializeField] private AmmoType ammoType;
    [SerializeField] private LayerMask hitMask;
    [SerializeField] private AudioClip[] sfx;
    [SerializeField] private AudioClip gunClickSfx;
    
    private bool _isFiring;
    private Ammo _ammo;
    private double _timeToNextShot;
    private bool _shouldUpdateCounter;
    private AudioSource _audioSource;

    private void Awake()
    {
        _ammo = FindObjectOfType<Ammo>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _shouldUpdateCounter = true;
    }

    private void Update()
    {
        if (_isFiring)
        {
            Shoot();
        }

        if (_shouldUpdateCounter)
        {
            _ammo.UpdateAmmoCounter(ammoType);
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
        if(Time.time < _timeToNextShot) {return;}

        if (_ammo.GetAmmoCount(ammoType) <= 0)
        {
            _audioSource.PlayOneShot(gunClickSfx);
            _timeToNextShot = Time.time + 2f;  // TODO: Remove hardcoded delay
            return;
        }
        
        PlayShootFx();
        ProcessRaycast();
        _ammo.AddAmmo(ammoType, -1);
        _timeToNextShot = Time.time + (1f / shotsPerSecond);
    }

    private void PlayShootFx()
    {
        muzzleFlash.Play();
        
        if(sfx.Length <= 0) return;
        var audioClip = sfx[Random.Range(0, sfx.Length)];
        _audioSource.PlayOneShot(audioClip);
    }

    private void ProcessRaycast()
    {
        var fpCameraTransform = fpCamera.transform;
        if (!Physics.Raycast(fpCameraTransform.position, fpCameraTransform.forward, out var hit, range, hitMask)) return;

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
