using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public float moveSpeed = 1f;
    public IEnumerator MoveToTarget(Vector3 target)
    {
        while (transform.position != target)
        {
            // Move pet to the target point smoothly
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
