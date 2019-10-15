using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IKillable
{
    public int HitPoints { get; private set; }

    [SerializeField] private int baseHitPoints = 100;

    private void Start()
    {
        HitPoints = baseHitPoints;
    }

    public void TakeDamage(int damage)
    {
        // TODO add damage VFX+SFX
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        Debug.Log("You died");
    }
}
