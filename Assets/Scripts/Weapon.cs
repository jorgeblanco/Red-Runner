using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float range = 50f;
    [SerializeField] private int gunDamage = 20;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject hitFxPrefab;
    
    private bool _isFiring;

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
        PlayShootFx();
        ProcessRaycast();
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
        if (damageable != null)
        {
            damageable.Damage(gunDamage);
            Debug.Log(hit.collider.gameObject.name + " was damaged");
        }
        else
        {
            Debug.Log(hit.collider.gameObject.name + " was hit");
        }
    }

    private void PlayHitFx(RaycastHit hit)
    {
        var hitFx = Instantiate(hitFxPrefab, hit.point, Quaternion.identity, transform);
        hitFx.transform.forward = hit.normal;
    }
}
