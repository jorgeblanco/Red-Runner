using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable, IKillable
{
    public int HitPoints { get; private set; }

    [SerializeField] private int baseHitPoints = 100;
    [SerializeField] private float damageSfxVolume = 0.5f;
    [SerializeField] private AudioClip[] damageSfx;
    [SerializeField] private float deathSfxVolume = 0.5f;
    [SerializeField] private AudioClip[] deathSfx;

    private DeathHandler _deathHandler;
    private HealthCounter _healthCounter;
    private DisplayDamage _displayDamage;
    private AudioSource _audioSource;

    private void Start()
    {
        HitPoints = baseHitPoints;
        _deathHandler = GetComponent<DeathHandler>();
        _displayDamage = FindObjectOfType<DisplayDamage>();
        _healthCounter = FindObjectOfType<HealthCounter>();
        _healthCounter.UpdateCounter(HitPoints);
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            if (damageSfx.Length > 0)
            {
                var sfx = damageSfx[Random.Range(0, damageSfx.Length)];
                _audioSource.PlayOneShot(sfx, damageSfxVolume);
            }
            _displayDamage.ShowDamage();
        }
        HitPoints -= damage;
        _healthCounter.UpdateCounter(HitPoints);
        if (HitPoints <= 0)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (deathSfx.Length > 0)
        {
            var sfx = deathSfx[Random.Range(0, deathSfx.Length)];
            _audioSource.PlayOneShot(sfx, deathSfxVolume);
        }
        _deathHandler.HandleDeath();
    }
}
