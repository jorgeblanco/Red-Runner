using System;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private AmmoSlot[] ammoSlots = new AmmoSlot[Enum.GetNames(typeof(AmmoType)).Length];
    private AmmoCounter _ammoCounter;
    
    [Serializable]
    private class AmmoSlot
    {
        public AmmoType ammoType;
        public int ammoCount;
    }

    private void Awake()
    {
        _ammoCounter = FindObjectOfType<AmmoCounter>();
        for(var i = 0; i < ammoSlots.Length; i++)
        {
            ammoSlots[i].ammoType = (AmmoType) i;
        }
    }

    public void AddAmmo(AmmoType slot, int count)
    {
        ammoSlots[(int) slot].ammoCount += count;
        UpdateAmmoCounter(slot);
    }

    public int GetAmmoCount(AmmoType slot)
    {
        return ammoSlots[(int) slot].ammoCount;
    }

    public void UpdateAmmoCounter(AmmoType slot)
    {
        _ammoCounter.UpdateCounter(ammoSlots[(int) slot].ammoType, ammoSlots[(int) slot].ammoCount);
    }
}
