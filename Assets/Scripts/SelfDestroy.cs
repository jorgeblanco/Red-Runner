using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    [SerializeField] private float delay = 5f;
    void Start()
    {
        Destroy(gameObject, delay);
    }
}
