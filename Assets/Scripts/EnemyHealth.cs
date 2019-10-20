using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] private int baseHitPoints = 100;
    public int HitPoints { get; private set; }

    private void Start()
    {
        HitPoints = baseHitPoints;
    }

    public void TakeDamage(int damage)
    {
        HitPoints -= damage;
        if (HitPoints <= 0)
        {
            Kill();
        }
        BroadcastMessage("OnDamageTaken");
    }

    public void Kill()
    {
        Debug.Log(gameObject.name + " was killed");
        BroadcastMessage("OnDeath");
    }
}

