using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothing : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] private ParticleSystem smokeEffect;
    [SerializeField] private AudioSource smokeAlarmSource;
    public SpriteRenderer smokeAlarmSpriteRenderer;

    public CageControl cageControl;

    public Transform ghostTransform;
    public Transform heaterTransform;

    private bool isCarried = false;
    private bool isOnHeater = false;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isOnHeater)
        {
            float distanceToGhost = Vector3.Distance(transform.position, ghostTransform.position);

            // Check if the ghost is close enough to interact
            if (distanceToGhost < 1.5f)
            {
                if (!isCarried)
                {
                    // Attach the clothing to the ghost
                    transform.parent = ghostTransform;
                    transform.localPosition = Vector3.zero;
                    isCarried = true;
                }
                else
                {
                    // Check if the ghost is near the heater to drop the clothing
                    float distanceToHeater = Vector3.Distance(transform.position, heaterTransform.position);
                    if (distanceToHeater < 1.5f) // Adjust the distance as needed
                    {
                        transform.parent = null; // Detach the clothing
                        transform.position = heaterTransform.position; // Place it on the heater
                        isCarried = false;
                        isOnHeater = true;

                        // Start the heater effect and the timer for the fire alarm
                        StartCoroutine(StartHeaterEffect());
                    }
                }
            }
        }
    }

    IEnumerator StartHeaterEffect()
    {
        // Check if the heater effect (steam) is not already playing
        if (!smokeEffect.isPlaying)
        {
            smokeEffect.Play(); // Start the steam effect
        }

        yield return new WaitForSeconds(5); // Simulate time for the cloth to start smoking

        // After 5 seconds, stop the steam effect to simulate the cloth being burned
        smokeEffect.Stop();
        StartCoroutine(FlashSmokeAlarm());
        cageControl.StartFalling();
        smokeAlarmSource.Play();

        // Trigger the fire alarm sequence here
        PuzzleManager.Instance.CompleteFireAlarmPuzzle();
    }

    IEnumerator FlashSmokeAlarm()
    {
        bool isRed = false;
        Color32 redColor = new Color32(204, 0, 0, 255);
        Color32 whiteColor = Color.white;

        while (true) // Infinite loop, break it when you want to stop flashing
        {
            // Toggle color
            isRed = !isRed;
            smokeAlarmSpriteRenderer.color = isRed ? redColor : whiteColor;

            // Wait for 0.5 seconds before changing the color again
            yield return new WaitForSeconds(0.5f);
        }
    }
}