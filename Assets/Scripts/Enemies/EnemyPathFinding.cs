// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Define the EnemyPathFinding class
public class EnemyPathFinding : MonoBehaviour
{
    // Serialize the field for move speed, making it editable in the Unity editor
    [SerializeField] private float moveSpeed = 2f;

    // Declare private variables for the Rigidbody2D and movement direction
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private KnockBack knockBack;

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        // Get a reference to the Rigidbody2D component attached to the same GameObject
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        if (knockBack.GettingKnockedBack) { return; }
        // Move the GameObject's position based on the calculated movement direction and speed
        rb.MovePosition(rb.position + moveDir * (moveSpeed * Time.fixedDeltaTime));
    }

    // Public method to set the movement direction towards a target position
    public void MoveTo(Vector2 targetPosition)
    {
        // Set the movement direction to the provided target position
        moveDir = targetPosition;
    }
}