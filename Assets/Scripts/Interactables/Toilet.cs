using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : MonoBehaviour, IInteractable
{
    public Transform petRunToPoint; // The point to which the pet will run after flushing
    public GameObject pet; // Reference to the pet GameObject
    public AudioSource flushSound; // Reference to the AudioSource component

    public void Interact()
    {
        // Ensure the previous puzzle (Fire Alarm Puzzle) is completed before proceeding
        if (!PuzzleManager.Instance.IsFireAlarmPuzzleComplete) return;

        // Play the flushing sound
        flushSound.Play();

        // Start the pet's smooth movement to the designated point
        StartCoroutine(MovePetToTarget(pet.transform, petRunToPoint.position, 3f)); // Adjust speed as needed

        // Mark this puzzle as complete
        PuzzleManager.Instance.CompleteToiletPuzzle();
    }

    private IEnumerator MovePetToTarget(Transform pet, Vector3 target, float speed)
    {
        while (Vector3.Distance(pet.position, target) > 0.05f) // Keep moving until the pet is close enough to the target
        {
            pet.position = Vector3.MoveTowards(pet.position, target, speed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }
    }
}
