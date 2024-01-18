using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAnim : MonoBehaviour
{
    private ParticleSystem ps; // Reference to the ParticleSystem component

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        ps = GetComponent<ParticleSystem>(); // Get the ParticleSystem component attached to the same GameObject
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the ParticleSystem is valid and no longer alive
        if (ps && !ps.IsAlive())
        {
            DestroySelf(); // Call the method to destroy the GameObject
        }
    }

    // Method to destroy the GameObject
    public void DestroySelf()
    {
        Destroy(gameObject); // Destroy the GameObject this script is attached to
    }
}
