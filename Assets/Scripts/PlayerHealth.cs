using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IKillable
{
    public int HitPoints { get; private set; }

    [SerializeField] private int baseHitPoints = 100;

    private DeathHandler _deathHandler;
    private HealthCounter _healthCounter;
    private DisplayDamage _displayDamage;

    private void Start()
    {
        HitPoints = baseHitPoints;
        _deathHandler = GetComponent<DeathHandler>();
        _displayDamage = FindObjectOfType<DisplayDamage>();
        _healthCounter = FindObjectOfType<HealthCounter>();
        _healthCounter.UpdateCounter(HitPoints);
    }

    public void TakeDamage(int damage)
    {
        // TODO add damage SFX
        _displayDamage.ShowDamage();
        HitPoints -= damage;
        _healthCounter.UpdateCounter(HitPoints);
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
