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
        // Make the cage and the pet fall to their respective points
        while (transform.position != cageFallPoint.position || pet.transform.position != petFallPoint.position)
        {
            if (transform.position != cageFallPoint.position)
            {
                transform.position = Vector3.MoveTowards(transform.position, cageFallPoint.position, fallSpeed * Time.deltaTime);
                transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime); // Rotate the cage as it falls
            }
            if (pet.transform.position != petFallPoint.position)
            {
                pet.transform.position = Vector3.MoveTowards(pet.transform.position, petFallPoint.position, fallSpeed * Time.deltaTime);
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Brief pause after falling

        // Pet jumps out to a specific point
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
