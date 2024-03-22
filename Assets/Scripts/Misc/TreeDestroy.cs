
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
    public delegate void TreeDestroyedEventHandler();
    public static event TreeDestroyedEventHandler OnTreeDestroyed;



    private bool hasBeenDestroyed = false;
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
        if (!hasBeenDestroyed && currentHealth <= 0)
        {
            hasBeenDestroyed = true; // Set the flag to true to indicate that the tree has been destroyed

            Instantiate(deathVFXPrefab, transform.position, Quaternion.identity); // Instantiate death visual effects
            Vector2 currentPos = transform.transform.position;
            OnTreeDestroyed?.Invoke();
            Vector2 newPos = new Vector3(currentPos.x, currentPos.y - 1f);
            Instantiate(SpawnLog, transform.position, Quaternion.identity);
            Instantiate(SpawnStump, newPos, Quaternion.identity);
            Destroy(gameObject); // Destroy the enemy GameObject
        }
    }



    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.gameObject == ActiveWeapon.Instance.CurrentActiveWeapon.gameObject &&
    //      ActiveWeapon.Instance.CurrentActiveWeapon is Axe)
    //    {
    //        TakeDamage(1);
    //    }
    //}


}
  