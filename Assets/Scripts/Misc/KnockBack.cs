// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the KnockBack class
public class KnockBack : MonoBehaviour
{
    // Public property indicating whether the object is currently being knocked back
    public bool GettingKnockedBack { get; private set; }

    // Serialized field for the duration of the knockback effect
    [SerializeField] private float knockBackTime = 0.2f;

    // Reference to the Rigidbody2D component of the GameObject
    private Rigidbody2D rb;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get the Rigidbody2D component from the GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // Method to initiate the knockback effect
    public void GetKnockedBack(Transform damageSource, float knockBackThrust)
    {
        Debug.Log("Knockback initiated.");
        // Set the flag indicating that the object is currently being knocked back
        GettingKnockedBack = true;

        // Calculate the knockback direction based on the difference between this object's position and the damage source's position
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackThrust * rb.mass;

        // Apply an impulse force to the Rigidbody2D in the calculated direction
        rb.AddForce(difference, ForceMode2D.Impulse);

        // Start a coroutine to handle the duration of the knockback effect
        StartCoroutine(KnockRoutine());
    }

    // Coroutine to handle the duration of the knockback effect
    private IEnumerator KnockRoutine()
    {
        // Wait for the specified duration before resetting velocity and ending the knockback
        yield return new WaitForSeconds(knockBackTime);

        // Reset the velocity of the Rigidbody2D to zero
        rb.velocity = Vector2.zero;

        // Set the flag indicating that the object is no longer being knocked back
        GettingKnockedBack = false;
    }
}
