// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Define the PlayerController class
public class PlayerController : Singleton<PlayerController>
{
    // Public property to check if the player is facing left
    public bool FacingLeft { get { return facingLeft; } }


    // Serialize the field for move speed, making it editable in the Unity editor
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer myTrailRenderer;

    // Declare private variables for player controls, movement, Rigidbody2D, Animator, and SpriteRenderer
    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private float startingMoveSpeed;

    private bool facingLeft = false;
    private bool isDashing = false;

    // Awake is called when the script instance is being loaded
    protected override void Awake()
    {
        base.Awake();
        // Initialize player controls and get references to Rigidbody2D, Animator, and SpriteRenderer
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Subscribe to the Dash action in the Combat control scheme
        playerControls.Combat.Dash.performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Enable the player controls
        playerControls.Enable();
    }

    // Update is called once per frame
    private void Update()
    {
        // Call the method to handle player input
        PlayerInput();
    }

    // FixedUpdate is called every fixed framerate frame
    private void FixedUpdate()
    {
        // Call methods to adjust player facing direction and move the player
        AdjustPlayerFacingDirection();
        Move();
    }

    // Method to handle player input
    private void PlayerInput()
    {
        // Read the movement input from the controls
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        // Set animator parameters based on movement input
        myAnimator.SetFloat("MoveX", movement.x);
        myAnimator.SetFloat("MoveY", movement.y);
    }

    // Method to move the player
    private void Move()
    {
        // Move the player using Rigidbody2D and the calculated movement vector
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    // Method to adjust the player's facing direction based on the mouse position
    private void AdjustPlayerFacingDirection()
    {

        //----------------------Mouse only input------------------------//
        // Get the mouse position and the player's position in screen coordinates
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);

        // Check if the mouse is on the left or right side of the player and flip the sprite accordingly
        if (mousePos.x < playerScreenPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    // Method to initiate the player dash
    private void Dash()
    {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    // Coroutine to end the player dash
    private IEnumerator EndDashRoutine()
    {
        float dashTime = 0.2f;
        float dashCooldown = 0.25f;

        // Wait for the specified dash time
        yield return new WaitForSeconds(dashTime);

        // Reset move speed and trail renderer emission
        moveSpeed = startingMoveSpeed;
        myTrailRenderer.emitting = false;

        // Wait for the specified dash cooldown before allowing another dash
        yield return new WaitForSeconds(dashCooldown);

        isDashing = false;
    }
}
