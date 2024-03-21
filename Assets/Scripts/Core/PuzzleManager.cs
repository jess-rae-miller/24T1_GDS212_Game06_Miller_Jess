using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    // Use these flags to track the completion of each puzzle
    public bool IsFireAlarmPuzzleComplete { get; private set; }
    public bool IsToiletPuzzleComplete { get; private set; }
    public bool IsKeyPuzzleComplete { get; private set; }
    public bool IsRatPossessionComplete { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep this object persistent between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Methods to mark each puzzle as complete
    public void CompleteFireAlarmPuzzle()
    {
        IsFireAlarmPuzzleComplete = true;
        // Optionally, enable the next puzzle here
    }

    public void CompleteToiletPuzzle()
    {
        IsToiletPuzzleComplete = true;
        // Optionally, enable the next puzzle here
    }

    public void CompleteKeyPuzzle()
    {
        IsKeyPuzzleComplete = true;
        // Optionally, enable the next puzzle here
    }

    public void CompleteRatPossession()
    {
        IsRatPossessionComplete = true;
        // Optionally, enable the next puzzle or the game end sequence here
    }
}