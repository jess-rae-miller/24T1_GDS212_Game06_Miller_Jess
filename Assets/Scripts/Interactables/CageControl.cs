using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageControl : MonoBehaviour
{
    public Transform cageFallPoint; // Assign where the cage should fall
    public Transform petFallPoint; // Assign where the pet should fall to, distinct from the cage
    public Transform petJumpOutPoint; // After falling, where the pet jumps out to
    public Transform petBathroomPoint; // Final destination in the bathroom
    public GameObject pet; // Assign the pet GameObject

    private float fallSpeed = 5f; // Speed of the cage and pet falling, adjust as needed
    private float rotateSpeed = 90f; // Rotation speed of the cage in degrees per second
    private float petMoveSpeed = 5f; // Speed of the pet moving, adjust as needed

    public void StartFalling()
    {
        StartCoroutine(FallAndMovePetSequence());
    }

    IEnumerator FallAndMovePetSequence()
    {
        // Determine the target rotation for when the cage has fallen
        Quaternion targetRotation = Quaternion.Euler(0, 0, 90); // Adjust this target rotation as needed

        // Make the cage and the pet fall to their respective points
        while (transform.position != cageFallPoint.position || pet.transform.position != petFallPoint.position)
        {
            if (transform.position != cageFallPoint.position)
            {
                // Move the cage towards its fall point
                transform.position = Vector3.MoveTowards(transform.position, cageFallPoint.position, fallSpeed * Time.deltaTime);

                // Rotate the cage towards its target rotation at the same time
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            }

            if (pet.transform.position != petFallPoint.position)
            {
                // Move the pet towards its fall point
                pet.transform.position = Vector3.MoveTowards(pet.transform.position, petFallPoint.position, fallSpeed * Time.deltaTime);
            }
            yield return null;
        }

        // Wait for 2 seconds after the cage has settled
        yield return new WaitForSeconds(2);

        // Pet jumps out to a specific point after the cage has tipped
        yield return MoveToTarget(pet.transform, petJumpOutPoint.position, petMoveSpeed);

        // Pet moves to the bathroom point
        yield return MoveToTarget(pet.transform, petBathroomPoint.position, petMoveSpeed);
    }

    IEnumerator MoveToTarget(Transform objectToMove, Vector3 target, float speed)
    {
        while (objectToMove.position != target)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, target, speed * Time.deltaTime);
            yield return null;
        }
    }
}
