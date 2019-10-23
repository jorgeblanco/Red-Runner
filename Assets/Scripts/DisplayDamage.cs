using UnityEngine;
using Random = UnityEngine.Random;

public class DisplayDamage : MonoBehaviour
{
    [SerializeField] private float damageDecayTime = 3f;
    [SerializeField] private CanvasGroup[] damageFx;
    private bool _isHurt;

    public void ShowDamage()
    {
        var damageId = Random.Range(0, damageFx.Length);
        var damageImg = damageFx[damageId];
        damageImg.alpha = 1;
        _isHurt = true;
    }

    private void Update()
    {
        if (!_isHurt) return;
        
        _isHurt = false;
        foreach (var damageImg in damageFx)
        {
            damageImg.alpha = Mathf.Clamp01(damageImg.alpha - (1 / damageDecayTime) * Time.deltaTime);
            _isHurt = _isHurt || damageImg.alpha > 0;
        }
    }
}
