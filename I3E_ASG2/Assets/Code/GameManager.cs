using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool HasWrench { get; private set; } = false;
    public bool HasKey { get; private set; } = false;

    private bool isGamePaused = false;
    public GameObject pauseMenuUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scene loads
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance exists
        }
    }

    void Start()
    {
        // Ensure the cursor is visible and unlocked in the main menu
        ShowCursor();
    }

    public void ShowCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Method to lock cursor when entering indoor scenes
    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Method to unlock cursor when entering main menu or other scenes that need it
    public void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Method to restart the game (reload the current scene)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        ResumeGame(); // Ensure game is resumed after restart
        CoinManager.Instance.ResetCoinCount();
    }

    // Method to load a specific scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause game time
        isGamePaused = true;

        // Show pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }

        // Show cursor
        ShowCursor();
    }

    // Method to resume the game
    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume game time
        isGamePaused = false;

        // Hide pause menu UI
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Lock cursor if not in pause menu
        if (!SceneManager.GetActiveScene().name.Contains("Menu")) // Adjust condition as per your menu scene name
        {
            HideCursor();
        }
    }

    // Toggle pause game
    public void TogglePause()
    {
        if (isGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    // Ensure the wrench state is persisted across scenes
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)
        {
            LockCursor();
        }
        else
        {
            UnlockCursor(); // Unlock cursor for other scenes
        }

        // Ensure game is resumed if paused when transitioning scenes
        if (isGamePaused)
        {
            ResumeGame();
        }
    }

    // Method to pick up the wrench
    public void PickUpWrench()
    {
        HasWrench = true;
        Debug.Log("Wrench picked up.");
    }

    public void PickUpKey()
    {
        HasKey = true;
        Debug.Log("Key picked up.");
    }
}

