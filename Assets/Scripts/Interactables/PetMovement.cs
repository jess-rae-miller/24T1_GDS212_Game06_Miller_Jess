using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetMovement : MonoBehaviour
{
    public Transform[] movePoints;
    public float moveSpeed = 50f;
    public AudioSource audioSource;
    public AudioClip successClip;
    public Image fadePanel;
    public string nextSceneName = "EndScene";
    public float jumpHeight = 2f;

    private void Start()
    {
       
    }

    public void StartMovementSequence()
    {
        StartCoroutine(MoveSequence());
    }

    IEnumerator MoveSequence()
    {
        foreach (var point in movePoints)
        {
            yield return MoveToTarget(point.position);
        }

        // Actions after reaching the last point
        Vector3 landTarget = new Vector3(transform.position.x, transform.position.y - jumpHeight, transform.position.z);
        StartCoroutine(MoveToTarget(landTarget));

        audioSource.PlayOneShot(successClip);
        yield return new WaitForSeconds(successClip.length);
        yield return StartCoroutine(FadeToBlack());
        SceneManager.LoadScene(nextSceneName);
    }

     public IEnumerator MoveToTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeToBlack()
    {
        float elapsedTime = 0;
        Color initialColor = fadePanel.color;
        while (elapsedTime < 2f) // Fade duration
        {
            elapsedTime += Time.deltaTime;
            fadePanel.color = new Color(initialColor.r, initialColor.g, initialColor.b, Mathf.Clamp01(elapsedTime / 2f));
            yield return null;
        }
    }
}
