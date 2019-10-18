using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float zoomedFov;
    [SerializeField] private float regularFov;
    [SerializeField] private Transform basePos;
    [SerializeField] private Transform zoomedPos;
    [SerializeField] private Image reticle;

    private bool _toggleZoom;
    private bool _zoomed;
    
    private void Update()
    {
        GetInput();
        if (_toggleZoom)
        {
            ToggleZoom();
        }
    }

    private void ToggleZoom()
    {
        _zoomed = !_zoomed;
        fpCamera.fieldOfView = _zoomed ? zoomedFov : regularFov;
        transform.position = _zoomed ? zoomedPos.position : basePos.position;
        reticle.enabled = !_zoomed;
    }

    private void GetInput()
    {
        _toggleZoom = Input.GetButtonDown("Fire2");
    }
}
