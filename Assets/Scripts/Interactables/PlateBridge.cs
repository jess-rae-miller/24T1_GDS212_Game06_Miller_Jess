using UnityEngine;

public class PlateBridge : MonoBehaviour, IInteractable
{
    public GameObject bridge;

    public void Interact()
    {
        // Trigger animation or effects to show the plates moving to form a bridge
        Debug.Log("Plates move to form a bridge!");
        // Enable the bridge object or components that allow the pet to cross
        bridge.SetActive(true);
    }
}
