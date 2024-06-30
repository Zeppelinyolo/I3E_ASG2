using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchStateManager : MonoBehaviour
{
    public static WrenchStateManager Instance { get; private set; }

    private bool hasWrench = false;

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

    public void SetWrenchState(bool state)
    {
        hasWrench = state;
    }

    public bool IsWrenchPickedUp()
    {
        return hasWrench;
    }
}
