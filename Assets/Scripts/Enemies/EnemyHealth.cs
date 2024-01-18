using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3; // Initial health of the enemy
    [SerializeField] private GameObject deathVFXPrefab; // Prefab for the death visual effects
    [SerializeField] private float knockBackThrust = 15f; // Force applied to the enemy upon taking damage

    private int currentHealth; // Current health of the enemy
    private KnockBack knockBack; // Reference to the KnockBack component for applying knockback effect
    private Flash flash; // Reference to the Flash component for visual feedback on damage

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        flash = GetComponent<Flash>(); // Get the Flash component attached to the same GameObject
        knockBack = GetComponent<KnockBack>(); // Get the KnockBack component attached to the same GameObject
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = startingHealth; // Set the initial health
    }

    // Method to apply damage to the enemy
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Decrease current health by the given damage value
        knockBack.GetKnockedBack(PlayerController.Instance.transform, knockBackThrust); // Apply knockback effect
        StartCoroutine(flash.FlashRoutine()); // Initiate flash effect to indicate damage
        StartCoroutine(CheckDetectDeathRoutine()); // Check if the enemy's health is zero or below
    }

    // Coroutine to delay the death detection after the flash effect is completed
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime()); // Wait for the flash effect to complete
        DetectDeath(); // Check if the enemy's health is zero or below after the flash effect
    }

    // Method to detect if the enemy has died
    public void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity); // Instantiate death visual effects
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }
}
