using System;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private float decayTime = 30f;
    [SerializeField] private float baseIntensity = 4f;
    [SerializeField] private float minAngle = 40f;
    [SerializeField] private float maxAngle = 80f;
    [SerializeField] private float maxCharge = 2f;
    
    private Light _spotLight;
    private float _remainingCharge;
    private BatteryCounter _batteryCounter;
    private bool _isOn;

    private void Start()
    {
        _spotLight = GetComponentInChildren<Light>();
        _batteryCounter = FindObjectOfType<BatteryCounter>();
        _spotLight.enabled = _isOn;
    }

    private void Update()
    {
        GetInput();
        if(!_isOn) return;
        
        _spotLight.intensity = Mathf.Lerp(0, baseIntensity, _remainingCharge);
        _spotLight.spotAngle = Mathf.Lerp(minAngle, maxAngle, _remainingCharge);
        if (_remainingCharge > 0)
        {
            _remainingCharge -= (1 / decayTime) * Time.deltaTime;
        }
        else
        {
            _spotLight.enabled = false;
            _isOn = false;
            _remainingCharge = 0;
        }
        _batteryCounter.UpdateCounter(GetFlashlightCharge());
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Flashlight"))
        {
            _isOn = !_isOn;
            _spotLight.enabled = _isOn;
        }
    }

    public void RechargeFlashlight(float percentage)
    {
        _spotLight.enabled = true;
        _isOn = true;
        _remainingCharge += percentage / 100;
        _remainingCharge = Mathf.Clamp(_remainingCharge, 0, maxCharge);
        // TODO: Play SFX
    }

    private int GetFlashlightCharge()
    {
        return Mathf.RoundToInt(_remainingCharge * 100);
    }
}
