using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Tutorial : MonoBehaviour
{
    [SerializeField] private string[] textLines;
    [SerializeField] private bool showOnce;
    [SerializeField] private bool shouldInterrupt;

    private bool _wasShown;
    private TutorialText _tutorialText;

    private void Start()
    {
        _tutorialText = FindObjectOfType<TutorialText>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerHealth>() == null) return;
        if(showOnce && _wasShown) return;
        
        _tutorialText.SetTutorialTextMulti(textLines, shouldInterrupt);
        _wasShown = true;
    }
}
