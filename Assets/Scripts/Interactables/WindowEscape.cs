using UnityEngine;

public class WindowEscape : MonoBehaviour, IInteractable
{
    public GameObject escapeRoute;

    public void Interact()
    {
        // Open the window to allow the pet's escape
        Debug.Log("Window opens, providing an escape route.");
        escapeRoute.SetActive(true);
        // Trigger the end of game or a cutscene
    }
}
