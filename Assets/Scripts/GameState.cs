using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    
    private int _score;
    private bool _shouldReload;
    private bool _shouldQuit;

    private void Start()
    {
        // UpdateScore();
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
