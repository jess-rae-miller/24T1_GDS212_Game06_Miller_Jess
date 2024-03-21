using System.Collections;
using UnityEngine;

public class PlateBridge : MonoBehaviour, IInteractable
{
    public Vector3 dropPositionOffset = new Vector3(0, -5f, 0);
    public float rotationDegrees = 30f;
    public float moveSpeed = 1f;
    public float rotateSpeed = 30f;
    public PetMovement petMovement; // Assign through the editor

    private bool hasInteracted = false;

    public void Interact()
    {
        if (!PuzzleManager.Instance.IsRatPossessionComplete) return;

        if (!hasInteracted)
        {
            StartCoroutine(MoveAndRotateShelf());
            hasInteracted = true;
        }
    }

    IEnumerator MoveAndRotateShelf()
    {
        Vector3 targetPosition = transform.position + dropPositionOffset;
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + rotationDegrees);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            yield return null;
        }

        // After moving and rotating the shelf, start the pet movement sequence
        petMovement.StartMovementSequence();
    }
}
