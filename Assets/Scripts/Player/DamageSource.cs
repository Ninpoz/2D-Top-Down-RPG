// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the DamageSource class
public class DamageSource : MonoBehaviour
{
    // Serialized field for the amount of damage this source inflicts
    [SerializeField] private int damageAmount = 1;

    // Called when another collider enters this collider's trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Attempt to get the EnemyHealth component from the collided object
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

        // If the EnemyHealth component is found, call the TakeDamage method to inflict damage
        // The null-conditional operator "?." ensures the method is called only if enemyHealth is not null
        enemyHealth?.TakeDamage(damageAmount);

        TreeDestroy treeDestroy = other.GetComponent<TreeDestroy>();

        treeDestroy?.TakeDamage(damageAmount);

       
    }
}
