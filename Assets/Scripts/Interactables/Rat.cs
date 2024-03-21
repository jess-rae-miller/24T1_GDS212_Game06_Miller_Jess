using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour, IInteractable
{
    [SerializeField] private Collider2D ratCollider;
    public GameObject pet;
    public Transform mouseHoleExit;
    public Transform petRunAwayPoint;
    private GameObject ghost;
    private bool isPossessed = false;
    public float moveSpeed = 8f;
    private bool nearMouseHole = false;

    void Start()
    {
        ghost = GameObject.FindWithTag("Ghost");
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
                TeleportRat();
            }
        }
    }

    private void TeleportRat()
    {
        transform.position = mouseHoleExit.position; // Teleport the rat

        // Trigger the pet to run away if the rat teleports for the first time
        if (pet != null && petRunAwayPoint != null)
        {
            PetMovement petMovement = pet.GetComponent<PetMovement>();
            if (petMovement != null)
            {
                petMovement.StartCoroutine(petMovement.MoveToTarget(petRunAwayPoint.position));
            }
        }
    }

    public void Interact()
    {
        if (!PuzzleManager.Instance.IsKeyPuzzleComplete) return;
        // Code for possession, including disabling the ghost and setting camera target
        isPossessed = true;
        ghost.SetActive(false);
        ratCollider.isTrigger = false;
        Camera.main.GetComponent<CameraFollow>().SetTarget(transform);

        PuzzleManager.Instance.CompleteRatPossession();
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
