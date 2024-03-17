using UnityEngine;

public class Rat : MonoBehaviour, IInteractable
{
    public GameObject pet;
    public Transform targetPosition; // Target position for the pet to move to

    public void Interact()
    {
        // Trigger rat animation or effects to indicate possession
        Debug.Log("Rat has been possessed and scares the pet!");
        // Move the pet to the target position (e.g., dining table)
        pet.transform.position = targetPosition.position;
    }
}
