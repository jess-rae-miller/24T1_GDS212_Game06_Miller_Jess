using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance { get; private set; }

    // Flags for puzzle completion state
    public bool IsClothingOnHeaterComplete = false;
    public bool IsToiletFlushed = false;
    public bool IsKeysDelivered = false;
    public bool IsRatPossessionComplete = false;
    public bool IsPlateBridgeFormed = false;

    private void Awake()
    {
        // Implementing Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to call when clothing is successfully placed on the heater
    public void CompleteClothingOnHeater()
    {
        IsClothingOnHeaterComplete = true;
        Debug.Log("Clothing on Heater puzzle completed.");
    }

    // Add similar methods for other puzzle completion flags as needed
}
