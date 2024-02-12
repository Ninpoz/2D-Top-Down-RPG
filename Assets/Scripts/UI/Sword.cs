// Import necessary libraries
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

// Define the Sword class
public class Sword : MonoBehaviour, IWeapon
{
    // Declare private variables for player controls, animator, player controller, and active weapon
    [SerializeField] private GameObject slashAnimPrefab; // Prefab for the slashing animation
    [SerializeField] private Transform slashAnimSpawnPoint; // Spawn point for the slashing animation
    [SerializeField] private float swordAttackCD = 0.5f; // Cooldown time for sword attacks

    private Transform weaponCollider;
    private Animator myAnimator; // Animator component reference

   

    private GameObject slashAnim; // Instance of the slashing animation

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        myAnimator = GetComponent<Animator>(); 
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashSpawnPoint").transform;
    }



    // Update is called once per frame
    private void Update()
    {
        // Call the method to make the sword follow the mouse with an offset
        MouseFollowWithOffset();
       
    }
 
    // Method to handle the Attack action
    public void Attack()
    {
       
            // Trigger the "Attack" animation in the Animator
            //isAttacking = true;
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        
    }

    // Coroutine for the attack cooldown
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCD);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
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

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Event called during the swing-down animation to flip the slashing animation
    public void SwingDownFlipAnimEvent()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    // Method to make the sword follow the mouse with an offset
    private void MouseFollowWithOffset()
    {
        // Get the mouse position and the player's position in screen coordinates
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        // Calculate the angle between the player and the mouse position
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        // Adjust the rotation of the active weapon based on the mouse position
        if (mousePos.x < playerScreenPoint.x)
        {
           ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, 0);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

  
}
