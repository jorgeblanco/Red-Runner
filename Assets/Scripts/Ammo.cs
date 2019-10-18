using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] private int ammoCount = 100;

    public void AddAmmo(int count)
    {
        ammoCount += count;
    }

    public int GetAmmoCount()
    {
        return ammoCount;
    }
}
