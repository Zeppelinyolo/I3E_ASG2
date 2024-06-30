using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;

    public TMP_Text healthText;
    public GameObject deathMenuUI;
    public AudioClip deathSound;
    private AudioSource audioSource;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        deathMenuUI.SetActive(false); //death menu is hidden at start
        LockCursor();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("nosound");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        UpdateHealthUI();
        CheckIfDead();
    }

    void UpdateHealthUI()
    {
        healthText.text = "HP: " + currentHealth;
    }

    void CheckIfDead()
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Player is Dead!");

            if (audioSource != null && deathSound != null)
            {
                audioSource.PlayOneShot(deathSound);
            }
            else
            {
                Debug.LogWarning("Death sound or AudioSource is not set.");
            }

            CoinManager.Instance.ResetCoinCount();

            deathMenuUI.SetActive(true);
            //Pause the game
            Time.timeScale = 0f;
            UnlockCursor();
        }
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
