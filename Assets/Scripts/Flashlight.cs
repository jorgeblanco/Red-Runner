using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float decayTime = 30f;
    [SerializeField] private float baseIntensity = 4f;
    [SerializeField] private float minAngle = 40f;
    [SerializeField] private float maxAngle = 80f;
    
    private Light _spotLight;
    private float _remainingCharge = 1f;

    private void Start()
    {
        _spotLight = GetComponentInChildren<Light>();
    }

    private void Update()
    {
        _spotLight.intensity = Mathf.Lerp(0, baseIntensity, _remainingCharge);
        _spotLight.spotAngle = Mathf.Lerp(minAngle, maxAngle, _remainingCharge);
        if (_remainingCharge > 0)
        {
            _remainingCharge -= (1 / decayTime) * Time.deltaTime;
        }
        else
        {
            _spotLight.enabled = false;
            _remainingCharge = 0;
        }
    }

    public void RechargeFlashlight(float percentage)
    {
        _spotLight.enabled = true;
        _remainingCharge += percentage / 100;
        // TODO: Play SFX
    }

    public int GetFlashlightCharge()
    {
        return Mathf.RoundToInt(_remainingCharge * 100);
    }
}
