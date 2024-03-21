using System.Collections;
using UnityEngine;

public class Keychain : MonoBehaviour, IInteractable
{
    public Transform carryPoint;
    public GameObject pet;
    public Transform petRunToPoint;
    public Transform dropArea; // The area where the key should be dropped
    public float followSpeed = 2f;
    private bool isCarried = false;
    private bool hasBeenDropped = false;
    private Collider2D keychainCollider;

    void Start()
    {
        keychainCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isCarried && !hasBeenDropped)
        {
            // Make the keychain follow the carryPoint smoothly
            transform.position = Vector3.Lerp(transform.position, carryPoint.position, followSpeed * Time.deltaTime);
        }
    }

    public void Interact()
    {
        if (!isCarried && !hasBeenDropped && PuzzleManager.Instance.IsToiletPuzzleComplete)
        {
            PickUp();
        }
    }

    private void PickUp()
    {
        isCarried = true;
        // You could disable the collider here if you want to avoid any physical interaction after picking up
        keychainCollider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KeychainDropArea") && isCarried)
        {
            Drop();
        }
    }

    private void Drop()
    {
        if (hasBeenDropped) return; // Prevent the method from running more than once

        isCarried = false;
        hasBeenDropped = true;
        // Since we're not using physics to drop the key anymore, move it to a specific spot, like on a table
        transform.position = dropArea.position; // Assuming dropArea is the final resting place of the key
        keychainCollider.isTrigger = true; // Keep it as trigger if you want it to land on objects without physical collision

        // Trigger pet to run to the next point
        if (pet != null && petRunToPoint != null)
        {
            PetMovement petMovement = pet.GetComponent<PetMovement>();
            if (petMovement != null)
            {
                StartCoroutine(petMovement.MoveToTarget(petRunToPoint.position));
            }
        }

        // Mark the key puzzle as complete
        PuzzleManager.Instance.CompleteKeyPuzzle();
    }
}