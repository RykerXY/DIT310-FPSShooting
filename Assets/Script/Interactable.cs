using TMPro;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Camera playerCamera;
    public TextMeshProUGUI interactionText;
    public float interactionDistance = 2.0f;
    private IInteractable currentInteractable;
    public AudioSource audioSource;
    public AudioClip CleanSound;

    public void Update()
    {
        UpdateCurrentInteractable();
        UpdateInteractableText();
        CheckForInteractionInput();
    }

    void UpdateCurrentInteractable()
    {
        RaycastHit hit;
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance);
        currentInteractable = hit.collider?.GetComponent<IInteractable>();
    }
    
    void UpdateInteractableText()
    {
        if(currentInteractable == null)
        {
            interactionText.text = string.Empty;
            return;
        }
        
        interactionText.text = currentInteractable.interactMessage;
    }

    void CheckForInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            audioSource.PlayOneShot(CleanSound);
            currentInteractable.Interact();
        }
    }
}
