using UnityEngine;

public class Excrement : MonoBehaviour, IInteractable
{
    public string interactMessage => "Press E to clean up";

    public void Interact()
    {
        Destroy(gameObject);
    }
}
