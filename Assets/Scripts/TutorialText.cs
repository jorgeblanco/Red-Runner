using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Fade time = Tutorial text length * this")]
    private float fadeTimeMultiplier = 0.25f;
    
    private TextMeshProUGUI _text;
    private CanvasGroup _canvasGroup;
    private float _fadeTime;
    private int _currentTextIndex;
    private readonly Queue<string> _textLines = new Queue<string>();

    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0;
    }

    public void SetTutorialTextMulti(string[] textLines, bool interrupt = false)
    {
        if (interrupt)
        {
            _textLines.Clear();
            _canvasGroup.alpha = 0;
        }
        foreach (var line in textLines)
        {
            _textLines.Enqueue(line);
        }
    }

    private void SetTutorialText(string text)
    {
        _text.SetText(text);
        _canvasGroup.alpha = 1;
        _fadeTime = text.Length * fadeTimeMultiplier;
    }

    private void Update()
    {
        if (_canvasGroup.alpha <= 0)
        {
            if(_textLines.Count <= 0) return;
            SetTutorialText(_textLines.Dequeue());
        }

        _canvasGroup.alpha -= 1 / _fadeTime * Time.deltaTime;
    }
}
