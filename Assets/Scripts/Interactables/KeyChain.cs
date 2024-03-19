using TMPro;
using UnityEngine;

public class KeyChain : MonoBehaviour, IInteractable
{
    public GameObject pet;
    public GameObject nextPuzzleObject;
    public Transform targetPosition; // Target position for the pet to move to

    public void Interact()
    {
        // Simulate jiggling keys to attract the pet's attention
        Debug.Log("KeyChain Interacted: The keys jiggle, leading the pet to the next room.");
        // Optionally, trigger an animation or sound effect here
        pet.transform.position = targetPosition.position;
        nextPuzzleObject.SetActive(true);
    }
}