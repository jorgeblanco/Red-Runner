using System;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private int _currentWeapon;

    private void Start()
    {
        SetActiveWeapon(_currentWeapon);
    }

    private void Update()
    {
        HandleInput();
    }

    private void SetActiveWeapon(int index)
    {
        _currentWeapon = index < 0 ? weapons.Length - 1 : index % weapons.Length;
        for (int i = 0; i < weapons.Length; i++)
        {
            weapons[i].gameObject.SetActive(i == _currentWeapon);
        }
    }

    private void HandleInput()
    {
        var mouseWheel = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(mouseWheel) > Mathf.Epsilon)
        {
            SetActiveWeapon(mouseWheel > 0 ? _currentWeapon + 1 : _currentWeapon - 1);
        }
    }
}
