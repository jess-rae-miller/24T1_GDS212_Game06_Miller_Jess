using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAlarm : MonoBehaviour, IInteractable
{
    public GameObject pet;
    public GameObject cage;
    public Transform targetPosition; // Target position for the pet to move to

    public void Interact()
    {
        // Simulate the alarm going off and the cage being knocked over
        Debug.Log("Alarm Clock Interacted: The alarm goes off!");
        // Add the logic to knock over the pet's cage
        // Temp deactivate the cage object as a placeholder
        cage.SetActive(false);
        pet.transform.position = targetPosition.position;
    }
}
