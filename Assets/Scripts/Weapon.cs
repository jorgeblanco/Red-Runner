using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float range = 50f;
    [SerializeField] private int gunDamage = 20;
    
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
        var fpCameraTransform = fpCamera.transform;
        if (!Physics.Raycast(fpCameraTransform.position, fpCameraTransform.forward, out var hit, range)) return;
        
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
}
