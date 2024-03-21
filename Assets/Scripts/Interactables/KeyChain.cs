using UnityEngine;

public class Keychain : MonoBehaviour
{
    public Transform carryPoint; // Assign in the Unity Editor
    public float followSpeed = 2f; // Adjust follow speed as needed
    private bool isCarried = false;
    private Collider2D keychainCollider;
    private Rigidbody2D rb;

    void Start()
    {
        keychainCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        // Make sure the Rigidbody2D starts in a suitable state
        rb.isKinematic = true; // Prevent it from being affected by physics while not carried
    }

    void Update()
    {
        if (isCarried)
        {
            // Smoothly interpolate the position of the keychain towards the carryPoint
            transform.position = Vector3.Lerp(transform.position, carryPoint.position, followSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.E) && !isCarried)
        {
            float distanceToGhost = Vector3.Distance(transform.position, carryPoint.position);
            if (distanceToGhost < 1.5f) // Adjust the pickup distance as needed
            {
                PickUp();
            }
        }
    }

    public void PickUp()
    {
        isCarried = true;
        keychainCollider.isTrigger = false; // Disable its trigger state
        rb.isKinematic = true; // Optionally keep it kinematic if it should not fall while being carried
        // You might want to adjust the keychain's localPosition if it's not visually appearing where you expect relative to the carryPoint.
        transform.position = carryPoint.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Entered trigger with: {other.gameObject.name}");
        if (other.CompareTag("KeychainDropArea") && isCarried)
        {
            Debug.Log("Dropping keychain");
            Drop();
        }
    }

    private void Drop()
    {
        isCarried = false;
        // Detach the keychain from the carry point, letting it naturally fall or rest on the ground
        transform.parent = null;
        rb.isKinematic = false; // Allow it to be affected by physics again
        rb.gravityScale = 1; // Ensure it falls under gravity
    }
}