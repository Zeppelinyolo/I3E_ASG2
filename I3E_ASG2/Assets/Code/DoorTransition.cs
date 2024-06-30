using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorTransition : MonoBehaviour
{
    public string outdoorSceneName;
    public float interactionRange = 3f;
    public LayerMask interactableLayer;
    public Camera playerCamera;
    public GameObject interactionText;
    public GameObject interactionText2;
    public AudioClip doorOpenSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (interactionText != null)
        {
            interactionText.SetActive(false);
        }
        if (interactionText2 != null)
        {
            interactionText2.SetActive(false);
        }

        if (playerCamera == null)
        {
            Debug.LogError("Player Camera is not assigned!");
        }
    }

    void Update()
    {
        // Perform raycast from the center of the camera
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            Debug.Log("Raycast hit: " + hit.collider.gameObject.name);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                Debug.Log("Looking at the door.");

                if (GameManager.Instance.HasKey)
                {
                    Debug.Log("Player has the key.");

                    if (interactionText != null)
                    {
                        interactionText.SetActive(true);
                    }

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (doorOpenSound != null && audioSource != null)
                        {
                            audioSource.PlayOneShot(doorOpenSound);
                        }

                        // Load the outdoor scene
                        SceneManager.LoadScene(outdoorSceneName);
                        Debug.Log("Door opened, transitioning to scene: " + outdoorSceneName);
                    }
                }
                else
                {
                    Debug.Log("Player does not have the key.");

                    if (interactionText2 != null)
                    {
                        interactionText2.SetActive(true);
                    }
                }
            }
            else
            {
                // Hide interaction text
                if (interactionText != null)
                {
                    interactionText.SetActive(false);
                }
                if (interactionText2 != null)
                {
                    interactionText2.SetActive(false); 
                }
            }
        }
        else
        {
            if (interactionText != null)
            {
                interactionText.SetActive(false); // Hide interaction text if no interactable objects are hit
            }
            if (interactionText2 != null)
            {
                interactionText2.SetActive(false); // Hide interaction text if no interactable objects are hit
            }
        }
    }
}
