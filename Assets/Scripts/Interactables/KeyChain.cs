using UnityEngine;

public class KeyChain : MonoBehaviour, IInteractable
{
    public GameObject nextPuzzleObject;

    public void Interact()
    {
        // Simulate jiggling keys to attract the pet's attention
        Debug.Log("KeyChain Interacted: The keys jiggle, leading the pet to the next room.");
        // Optionally, trigger an animation or sound effect here
        // For demonstration, we could enable the next puzzle object or trigger its behavior
        nextPuzzleObject.SetActive(true);
    }
}