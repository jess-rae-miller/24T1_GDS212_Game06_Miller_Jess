using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    public LayerMask interactableLayer; // Layer for interactable objects

    private Rigidbody2D rb;
    private Vector2 movement;
    private Camera cam;

    private float floatAmplitude = 1f;
    private float floatFrequency = 3f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    void Update()
    {
        // Handle movement input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Check for interactions
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        // Handle movement based on input
        if (movement != Vector2.zero)
        {
            rb.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);
        }
        else
        {
            // When idle, apply gentle vertical oscillation
            Float();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Float()
    {
        float verticalFloat = Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        rb.velocity = new Vector2(rb.velocity.x, verticalFloat);
    }

    void Interact()
    {
        // Detect interactable objects in front of the ghost
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 6f, interactableLayer);
        if (hit.collider != null)
        {
            Debug.Log("Interacted with " + hit.collider.name);
            hit.collider.gameObject.GetComponent<IInteractable>()?.Interact();
        }
    }
    public void ReactivateGhost()
    {
        // Reactivate the ghost (if it was deactivated during possession)
        gameObject.SetActive(true);

        // Assuming the camera should be re-parented to the ghost when it reactivates
        Camera.main.transform.SetParent(transform);
        Camera.main.transform.localPosition = new Vector3(0, 0, -10); // Reset the camera position relative to the ghost

        // Additional logic to resume normal ghost behavior and camera following
    }
}
public interface IInteractable
{
    void Interact();
}

