using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable, IKillable
{
    [SerializeField] private int baseHitPoints = 100;
    [SerializeField] private float damageSfxVolume = 0.5f;
    [SerializeField] private AudioClip[] damageSfx;
    [SerializeField] private float deathSfxVolume = 0.5f;
    [SerializeField] private AudioClip[] deathSfx;
    
    private bool _isDead;
    private AudioSource _audioSource;
    public int HitPoints { get; private set; }

    private void Start()
    {
        HitPoints = baseHitPoints;
        _audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage(int damage)
    {
        if(_isDead) return;
        
        if (damage > 0 && damageSfx.Length > 0)
        {
            var sfx = damageSfx[Random.Range(0, damageSfx.Length)];
            _audioSource.pitch = Random.Range(0.75f, 1.25f);
            _audioSource.PlayOneShot(sfx, damageSfxVolume);
        }
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
        
        if (deathSfx.Length > 0)
        {
            var sfx = deathSfx[Random.Range(0, deathSfx.Length)];
            _audioSource.pitch = Random.Range(0.75f, 1.25f);
            _audioSource.PlayOneShot(sfx, deathSfxVolume);
        }
        BroadcastMessage("OnDeath");
        _isDead = true;
    }
}

