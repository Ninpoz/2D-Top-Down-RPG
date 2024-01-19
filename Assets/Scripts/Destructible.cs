// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the Destructible class
public class Destructible : MonoBehaviour
{
    // Reference to the visual effect to be instantiated upon destruction
    [SerializeField] private GameObject destroyVFX;
    



    // Called when another collider enters the trigger collider attached to this object
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object has a DamageSource component
        if (other.gameObject.GetComponent<DamageSource>())
        {
            // Instantiate the destruction visual effect at the current position with no rotation
            Instantiate(destroyVFX, transform.position, Quaternion.identity);

            // Destroy the current game object (the destructible object)
            Destroy(gameObject);
        }
    }
}
