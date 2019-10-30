using UnityEngine;

public class InteriorLightingControl : MonoBehaviour
{
    [SerializeField] private Light interiorLight;
    [SerializeField] private Light exteriorLight;

    private void OnTriggerExit(Collider other)
    {
        interiorLight.enabled = false;
        exteriorLight.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        interiorLight.enabled = true;
        exteriorLight.enabled = false;
    }
}
