using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] private Camera fpCamera;
    [SerializeField] private float zoomedFov = 30f;
    [SerializeField] private float regularFov = 60f;
    [SerializeField] private Transform basePos;
    [SerializeField] private Transform zoomedPos;
    [SerializeField] private Image reticle;
    [SerializeField] private float zoomedSensitivity = 1f;
    [SerializeField] private float baseSensitivity = 2f;

    private bool _toggleZoom;
    private bool _zoomed;
    private RigidbodyFirstPersonController _fpController;

    private void Start()
    {
        _fpController = FindObjectOfType<RigidbodyFirstPersonController>();
    }

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
        _fpController.mouseLook.XSensitivity = _zoomed ? zoomedSensitivity : baseSensitivity;
        _fpController.mouseLook.YSensitivity = _zoomed ? zoomedSensitivity : baseSensitivity;
    }

    private void GetInput()
    {
        _toggleZoom = Input.GetButtonDown("Fire2");
    }
}
