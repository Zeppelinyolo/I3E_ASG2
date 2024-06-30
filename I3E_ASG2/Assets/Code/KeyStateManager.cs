using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyStateManager : MonoBehaviour
{
    public static KeyStateManager Instance { get; private set; }

    private bool hasKey = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetKeyState(bool state)
    {
        hasKey = state;
    }

    public bool IsKeyPickedUp()
    {
        return hasKey;
    }
}
