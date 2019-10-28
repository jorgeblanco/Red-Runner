using System;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private Canvas winCanvas;

    private FirstPersonController _fpController;

    private void Start()
    {
        gameOverCanvas.enabled = false;
        winCanvas.enabled = false;
        _fpController = FindObjectOfType<FirstPersonController>();
    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        StopGame();
    }

    public void HandleWin()
    {
        winCanvas.enabled = true;
        StopGame();
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        _fpController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
