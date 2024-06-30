using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public TMP_Text coinCounterText;
    private int coinCount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetCoinCount();
    }

    public void CollectCoin()
    {
        coinCount++;
        UpdateCoinCounterUI();
    }

    public void ResetCoinCount()
    {
        coinCount = 0;
        UpdateCoinCounterUI();
    }

    private void UpdateCoinCounterUI()
    {
        if (coinCounterText != null)
        {
            coinCounterText.text = "Coins: " + coinCount.ToString();
        }
        else
        {
            Debug.LogError("Coin counter text is not assigned.");
        }
    }
}
