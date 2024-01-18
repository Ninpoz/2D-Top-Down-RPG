// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

// Define the Sword class
public class Sword : MonoBehaviour
{
    // Declare private variables for player controls, animator, player controller, and active weapon
    [SerializeField] private GameObject slashAnimPrefab; // Prefab for the slashing animation
    [SerializeField] private Transform slashAnimSpawnPoint; // Spawn point for the slashing animation
    [SerializeField] private Transform weaponCollider; // Collider for the weapon
    [SerializeField] private float swordAttackCD = 0.5f; // Cooldown time for sword attacks

    private PlayerControls playercontrols; // Player controls reference
    private Animator myAnimator; // Animator component reference
    private PlayerController playercontroller; // PlayerController component reference
    private ActiveWeapon activeWeapon; // ActiveWeapon component reference
    private bool attackButtonDown, isAttacking = false; // Flags for attack input and current attacking state

    private GameObject slashAnim; // Instance of the slashing animation

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        // Get references to the PlayerController, ActiveWeapon, and Animator components in the parent GameObject
        playercontroller = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
        myAnimator = GetComponent<Animator>();

        // Initialize the PlayerControls
        playercontrols = new PlayerControls();
    }

    // OnEnable is called when the object becomes enabled and active
    private void OnEnable()
    {
        // Enable the PlayerControls
        playercontrols.Enable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Subscribe to the Attack action in the Combat control scheme
        playercontrols.Combat.Attack.started += _ => StartAttacking();
        playercontrols.Combat.Attack.canceled += _ => StopAttacking();
    }

    // Update is called once per frame
    private void Update()
    {
        // Call the method to make the sword follow the mouse with an offset
        MouseFollowWithOffset();
        Attack();
    }
    // Method to handle the start of the attack
    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    // Method to handle the end of the attack
    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    // Method to handle the Attack action
    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            // Trigger the "Attack" animation in the Animator
            isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
    }

    // Coroutine for the attack cooldown
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        isAttacking = false;
    }

    // Event called when the attack animation is done
    public void DoneAttackAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    // Event called during the swing-up animation to flip the slashing animation
    public void SwingUpFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (playercontroller.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Event called during the swing-down animation to flip the slashing animation
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playercontroller.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Method to make the sword follow the mouse with an offset
    private void MouseFollowWithOffset()
    {
        // Get the mouse position and the player's position in screen coordinates
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playercontroller.transform.position);

        // Calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // Adjust the rotation of the active weapon based on the mouse position
        if (mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

  
}
