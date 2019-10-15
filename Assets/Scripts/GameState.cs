using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    
    private bool _shouldReload;
    private int _score;

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
    }

    private void GetInput()
    {
        if (Input.GetButton("Fire2") && !_shouldReload)
        {
            _shouldReload = true;
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
