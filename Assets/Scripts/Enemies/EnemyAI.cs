// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Define the EnemyAI class
public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] private float roamChangeDirFloat = 2f;
    
    // Define the possible states for the enemy AI
    private enum State
    {
        Roaming
    }

    // Declare private variables for the current state and the EnemyPathFinding component
    private State state;
    private EnemyPathFinding enemyPathFinding;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get a reference to the EnemyPathFinding component attached to the same GameObject
        enemyPathFinding = GetComponent<EnemyPathFinding>();

        // Set the initial state to Roaming
        state = State.Roaming;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Start the coroutine for the Roaming state
        StartCoroutine(RoamingRoutine());
    }

    // Coroutine to handle the Roaming state
    private IEnumerator RoamingRoutine()
    {
        // Continue looping as long as the state is Roaming
        while (state == State.Roaming)
        {
            // Get a random position for the enemy to roam to
            Vector2 roamPosition = GetRoamingPosition();

            // Move the enemy to the roaming position using the EnemyPathFinding component
            enemyPathFinding.MoveTo(roamPosition);

            // Wait for 2 seconds before the next iteration
            yield return new WaitForSeconds(roamChangeDirFloat);
        }
    }

    // Method to get a random roaming position within a normalized range
    private Vector2 GetRoamingPosition()
    {
        // Generate a random Vector2 within the range of (-1, -1) to (1, 1) and normalize it
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
