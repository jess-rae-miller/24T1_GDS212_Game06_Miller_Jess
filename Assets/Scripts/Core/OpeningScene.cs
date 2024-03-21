using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OpeningScene : MonoBehaviour
{
    public Image fadePanel; // Assign a UI Image (black panel) in the Unity Editor
    public AudioSource audioSource; // Assign an AudioSource component in the Unity Editor
    public float delay = 5f; // Time in seconds before starting fade out

    private void Start()
    {
        StartCoroutine(BeginOpeningSequence());
    }

    IEnumerator BeginOpeningSequence()
    {
        // Ensure the panel is fully black and active at the start
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = Color.black;

        // Play your audio source here
        audioSource.Play();

        // Wait for the audio to finish, or a delay, whichever is longer
        yield return new WaitForSeconds(Mathf.Max(delay, audioSource.clip.length));

        // Start fading out
        yield return StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < delay)
        {
            // Gradually change the panel's alpha to 0
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, elapsedTime / delay);
            fadePanel.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadePanel.gameObject.SetActive(false); // Optionally disable the panel after fade out
    }
}
