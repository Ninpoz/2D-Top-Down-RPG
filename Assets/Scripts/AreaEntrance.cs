using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{
    [SerializeField] private string transitionName;

    // Start is called before the first frame update
    private void Start()
    {
        // Check if the transition name matches the current scene's transition name
        if (transitionName == SceneManagement.Instance.SceneTransitionName)
        {
            // Move the player to the entrance position
            PlayerController.Instance.transform.position = this.transform.position;

            // Set the camera to follow the player
            CameraController.Instance.SetPlayerCameraFollow();

            // Fade the UI to clear
            UIFade.Instance.FadeToClear();
        }
    }
}
