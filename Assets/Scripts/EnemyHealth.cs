using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] private int baseHitPoints = 100;
    private bool _isDead;
    public int HitPoints { get; private set; }

    private void Start()
    {
        HitPoints = baseHitPoints;
    }

    public void TakeDamage(int damage)
    {
        if(_isDead) return;
        
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            Kill();
        }
        BroadcastMessage("OnDamageTaken");
    }

    public void Kill()
    {
        if(_isDead) return;
        
        BroadcastMessage("OnDeath");
        _isDead = true;
    }
}

