using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image fadePanel;
    public float fadeDuration = 1f;

    public void PlayGame()
    {
        StartCoroutine(FadeToBlackAndLoadNextScene());
    }

    IEnumerator FadeToBlackAndLoadNextScene()
    {
        float elapsedTime = 0f;
        Color startColor = fadePanel.color;
        Color targetColor = new Color(0f, 0f, 0f, 1f); // Black with full alpha

        while (elapsedTime < fadeDuration)
        {
            fadePanel.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Load the next scene after the fade to black is complete
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        // Start the fade to clear routine after loading the next scene
        StartCoroutine(FadeToClearRoutine(startColor));
    }

    IEnumerator FadeToClearRoutine(Color startColor)
    {
        float elapsedTime = 0f;
        Color targetColor = new Color(0f, 0f, 0f, 0f); // Fully transparent

        while (elapsedTime < fadeDuration)
        {
            fadePanel.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
