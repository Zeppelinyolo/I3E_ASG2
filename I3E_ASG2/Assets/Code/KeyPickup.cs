using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPickup : MonoBehaviour
{
    public LayerMask playerLayer;
    public float interactionRange = 3f;
    public Camera playerCamera;
    public GameObject interactionText;
    public AudioClip pickupSound; 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
        else
        {
            Debug.LogError("Interaction Text is not assigned!");
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned!");
        }
        if (KeyStateManager.Instance.IsKeyPickedUp())
        {
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
                Debug.Log("Looking at the key object.");

                if (interactionText != null)
                {
                    interactionText.SetActive(true);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    GameManager.Instance.PickUpKey();

                    if (pickupSound != null && audioSource != null)
                    {
                        audioSource.PlayOneShot(pickupSound); 
                    }

                    if (interactionText != null)
                    {
                        interactionText.SetActive(false);
                    }
                    Destroy(gameObject); 
                    Debug.Log("Key picked up.");
                }
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
