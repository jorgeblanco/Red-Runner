using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IKillable
{
    public int HitPoints { get; private set; }

    [SerializeField] private int baseHitPoints = 100;

    private DeathHandler _deathHandler;

    private void Start()
    {
        HitPoints = baseHitPoints;
        _deathHandler = GetComponent<DeathHandler>();
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
        _deathHandler.HandleDeath();
    }
}
