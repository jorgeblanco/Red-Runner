using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    private DeathHandler _deathHandler;

    private void Start()
    {
        _deathHandler = FindObjectOfType<DeathHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _deathHandler.HandleWin();
    }
}
