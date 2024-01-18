using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat; // Material used for the flash effect
    [SerializeField] private float restoreDefaultMatTime = 0.2f; // Time it takes to restore the default material

    private Material defaultMat; // Default material of the SpriteRenderer
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer component

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component attached to the same GameObject
        defaultMat = spriteRenderer.material; // Store the default material
    }

    // Getter method to retrieve the time it takes to restore the default material
    public float GetRestoreMatTime()
    {
        return restoreDefaultMatTime;
    }

    // Coroutine for the flash effect
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteFlashMat; // Set the material to the white flash material
        yield return new WaitForSeconds(restoreDefaultMatTime); // Wait for the specified time
        spriteRenderer.material = defaultMat; // Restore the default material
    }
}
