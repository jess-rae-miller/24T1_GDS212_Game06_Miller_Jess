using UnityEngine;

public class GhostController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public LayerMask interactableLayer; // Layer for interactable objects

    private Rigidbody2D rb;
    private Vector2 movement;
    private Camera cam;

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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Interact();
        }
    }

    void FixedUpdate()
    {
        // Move the ghost
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Interact()
    {
        // Detect interactable objects in front of the ghost
        RaycastHit2D hit = Physics2D.Raycast(transform.position, movement, 4f, interactableLayer);
        if (hit.collider != null)
        {
            Debug.Log("Interacted with " + hit.collider.name);
            // Here, we will call the interact function of the object
            hit.collider.gameObject.GetComponent<IInteractable>()?.Interact();
        }
    }
}

// Define an interface for interactable objects
public interface IInteractable
{
    void Interact();
}
