using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour, IInteractable
{
    [SerializeField] private Collider2D ratCollider;
    public GameObject pet;
    public Transform mouseHoleExit; // Exit point for the mouse hole teleport
    private GameObject ghost;
    private bool isPossessed = false;
    public float moveSpeed = 8f;
    private bool nearMouseHole = false; // Tracks proximity to a mouse hole

    void Start()
    {
        ghost = GameObject.FindWithTag("Ghost"); // Ensure your Ghost is tagged appropriately
        ratCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (isPossessed)
        {
            float moveInput = Input.GetAxis("Horizontal");
            transform.Translate(Vector2.right * moveInput * Time.deltaTime * moveSpeed, Space.World);

            // Check for E key press and proximity to the mouse hole to teleport
            if (Input.GetKeyDown(KeyCode.E) && nearMouseHole)
            {
                transform.position = mouseHoleExit.position; // Teleport the rat
            }
        }
    }

    public void Interact()
    {
        // Code for possession, including disabling the ghost and setting camera target
        isPossessed = true;
        ghost.SetActive(false);
        ratCollider.isTrigger = false;

        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MouseHole"))
        {
            nearMouseHole = true;
        }
        else if (other.CompareTag("ReleaseArea")) // Assuming your trigger box is tagged as "ReleaseArea"
        {
            ReleasePossession();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("MouseHole"))
        {
            nearMouseHole = false;
        }
    }

    public void ReleasePossession()
    {
        // Code for releasing possession, including reactivating the ghost and resetting camera target
        isPossessed = false;
        ratCollider.isTrigger = true; // Optionally reset this based on your game's mechanics
        ghost.SetActive(true);
        ghost.transform.position = this.transform.position + Vector3.up * 2; // Or another appropriate position

        Camera.main.GetComponent<CameraFollow>().SetTarget(ghost.transform);
    }
}
