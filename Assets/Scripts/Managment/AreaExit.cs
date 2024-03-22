using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;            // The name of the scene to load
    [SerializeField] private string sceneTransitionName;   // The transition name associated with the scene

    private float waitToLoadTime = 1f;                      // Time to wait before loading the scene

    // Triggered when another collider enters this trigger collider
    private void OnTriggerEnter2D(Collider2D other)
    {

      
        // Check if the entering collider belongs to the player
        if (other.gameObject.GetComponent<PlayerController>())
        {

            // Set the transition name for the next scene
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);

            // Fade the UI to black
            UIFade.Instance.FadeToBlack();

            // Start the coroutine to wait and then load the scene
            StartCoroutine(LoadSceneRoutine());
        }
    }

    // Coroutine to wait for a specific time before loading the scene
    private IEnumerator LoadSceneRoutine()
    {
        // Continue the loop until the wait time is zero
        while (waitToLoadTime >= 0)
        {
            // Decrease the wait time by the time passed since the last frame
            waitToLoadTime -= Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Load the specified scene after the wait time has elapsed
        SceneManager.LoadScene(sceneToLoad);
    }
}
