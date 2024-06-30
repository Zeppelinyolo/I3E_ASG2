using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OutDoorTransition : MonoBehaviour
{
    public string indoorSceneName; // Name of the indoor scene to load
    public GameObject interactionText; // UI text element to display the prompt

    private bool isLookingAtDoor = false;

    void Start()
    {
        interactionText.gameObject.SetActive(false); // Hide interaction text initially
    }

    void Update()
    {
        // Example: Raycast logic to detect player interaction with the door
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Door"))
            {
                isLookingAtDoor = true;
                interactionText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    // Player interacts with the door
                    LoadIndoorScene();
                }
            }
            else
            {
                isLookingAtDoor = false;
                interactionText.gameObject.SetActive(false);
            }
        }
        else
        {
            isLookingAtDoor = false;
            interactionText.gameObject.SetActive(false);
        }
    }

    void LoadIndoorScene()
    {
        // Load the indoor scene by name
        SceneManager.LoadScene(indoorSceneName);
    }
}
