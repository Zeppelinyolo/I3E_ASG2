using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrenchPickup : MonoBehaviour
{
    public LayerMask playerLayer;
    public float interactionRange = 3f;
    public Camera playerCamera;
    public GameObject interactionText;
    public GameObject interactionText2;
    public GameObject interactionText3;



    void Start()
    {
        if (interactionText != null && interactionText2 != null && interactionText3 != null)
        {
            interactionText.SetActive(false);
            interactionText2.SetActive(true);
            interactionText3.SetActive(false);
        }
        else
        {
            Debug.LogError("txt");
        }

        if (playerCamera == null)
        {
            Debug.LogError("cam");
        }

        if (WrenchStateManager.Instance.IsWrenchPickedUp())
        {
            Debug.LogError("meow");
            gameObject.SetActive(false);
            interactionText.SetActive(false);
        }
    }

    void Update()
    {
        // Perform raycast from the center of the camera
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, playerLayer))
        {
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (interactionText != null)
                {
                    interactionText.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.Instance.PickUpWrench();
                    if (interactionText != null && interactionText2 != null && interactionText3 != null)
                    {
                        interactionText.SetActive(false);
                        interactionText2.SetActive(false);
                        interactionText3.SetActive(true);
                    }
                    Destroy(gameObject);
                    Debug.Log("wrench gotcha");
                }
                else
                {
                    if (interactionText != null)
                    {
                        interactionText.SetActive(false); // Hide interaction text if looking away from interactable objects
                    }
                }
            }
            else
            {
                if (interactionText != null)
                {
                    interactionText.SetActive(false); // Hide interaction text if no interactable objects are hit
                }
            }
        }
    }
}