
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class TreeDestroy : MonoBehaviour

{
    [SerializeField] private GameObject SpawnLog;
    [SerializeField] private GameObject SpawnStump;
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathVFXPrefab;
    
    
    
   
    public int currentHealth;
    private Flash flash;
    // Start is called before the first frame update

    private void Awake()
    {
        flash = GetComponent<Flash>(); 
        
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Decrease current health by the given damage value
        
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
            print("Inside death method");
            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity); // Instantiate death visual effects
            Vector2 currentPos = transform.transform.position;

            Vector2 newPos = new Vector3(currentPos.x, currentPos.y - 1f);
            Instantiate(SpawnLog, transform.position, Quaternion.identity);
            Instantiate(SpawnStump, newPos, Quaternion.identity);
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }

  

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    // Attempt to get the EnemyHealth component from the collided object
    //    EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

    //    // If the EnemyHealth component is found, call the TakeDamage method to inflict damage
    //    // The null-conditional operator "?." ensures the method is called only if enemyHealth is not null
    //    enemyHealth?.TakeDamage(damageAmount);
    //}


}
  