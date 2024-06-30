using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject howToPlayPanel;
    public GameObject creditsPanel;

    private void Start()
    {
        ShowMainMenu();
        UnlockCursor();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (howToPlayPanel.activeSelf || creditsPanel.activeSelf)
            {
                ShowMainMenu();
            }
        }
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(false);
        UnlockCursor();
    }

    public void ShowHowToPlay()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(true);
        creditsPanel.SetActive(false);
        UnlockCursor();
    }

    public void ShowCredits()
    {
        mainMenuPanel.SetActive(false);
        howToPlayPanel.SetActive(false);
        creditsPanel.SetActive(true);
        UnlockCursor();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("IndoorScene");
        LockCursor();
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
