using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class GameState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    
    private int _score;
    private bool _shouldReload;
    private bool _shouldQuit;
    private FirstPersonController _fpController;
    private WeaponSwitcher _weaponSwitcher;

    private void Start()
    {
        // UpdateScore();
        _weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        _fpController = FindObjectOfType<FirstPersonController>();
    }
    
    private void Update()
    {
        GetInput();
        HandleInput();
    }

    private void HandleInput()
    {
        if (_shouldReload)
        {
            Reload();
        }
        if (_shouldQuit)
        {
            Quit();
        }
    }

    private void GetInput()
    {
        if (Input.GetButton("Reload") && !_shouldReload)
        {
            _shouldReload = true;
        }
        if (Input.GetButton("Quit") && !_shouldQuit)
        {
            _shouldQuit = true;
        }
    }

    public void AddToScore(int scoreToAdd)
    {
        _score += scoreToAdd;
        UpdateScore();
    }

    private void UpdateScore()
    {
        score.SetText($"(Score] [{_score})");
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        _weaponSwitcher.enabled = true;
        _fpController.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings);
        Time.timeScale = 1;
        _weaponSwitcher = FindObjectOfType<WeaponSwitcher>();
        if (_weaponSwitcher != null)
        {
            _weaponSwitcher.enabled = true;
        }
        _fpController = FindObjectOfType<FirstPersonController>();
        if (_fpController != null)
        {
            _fpController.enabled = true;
        }
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    public void GoHome()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }
}
