using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 2f;

    void Start()
    {
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0); // Ensure transparent at start
    }

    public void StartFadeOut(string sceneToLoad)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneToLoad));
    }

    private IEnumerator FadeOutAndLoadScene(string sceneToLoad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);
            yield return null;
        }

        // Load the next scene after the fade completes
        SceneManager.LoadScene(sceneToLoad);
    }
}
