using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class EngineInteraction : MonoBehaviour
{
    public LayerMask playerLayer;
    public float interactionRange = 3f;
    public Camera playerCamera;
    public GameObject interactionText;
    public GameObject interactionText2;
    public GameObject gameCompletedUI;

    private bool engineRepaired = false;

    void Start()
    {
        if (interactionText == null)
        {
            Debug.LogError("Interaction Text is not assigned!");
        }

        if (interactionText2 == null)
        {
            Debug.LogError("Interaction Text is not assigned!");
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned!");
        }

        if (gameCompletedUI != null)
        {
            gameCompletedUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Game Completed UI is not assigned!");
        }
    }

    void Update()
    {
        // Perform raycast from the center of the camera
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, playerLayer))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject && !engineRepaired)
            {
                if (interactionText != null && interactionText2 != null)
                {
                    if (GameManager.Instance.HasWrench)
                    {
                        interactionText.SetActive(false);
                        interactionText2.SetActive(true);
                        Debug.Log("Wrench picked up. Displaying interactionText.");
                    }
                    else
                    {
                        interactionText.SetActive(true);
                        interactionText2.SetActive(false);
                        Debug.Log("Wrench not picked up. Displaying interactionText2.");
                    }
                }

                if (Input.GetKeyDown(KeyCode.E) && GameManager.Instance.HasWrench)
                {
                    Debug.Log("E key pressed. Attempting to repair engine.");
                    RepairEngine();
                }
            }
            else
            {
                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(false);
                }
                if (interactionText2 != null)
                {
                    interactionText2.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
            if (interactionText2 != null)
            {
                interactionText2.gameObject.SetActive(false);
            }
        }
    }

    void RepairEngine()
    {
        engineRepaired = true;
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
            interactionText2.gameObject.SetActive(false);

        }
        if (gameCompletedUI != null)
        {
            gameCompletedUI.SetActive(true);
            UnlockCursor();
        }
        Debug.Log("Engine repaired. Game completed.");
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

