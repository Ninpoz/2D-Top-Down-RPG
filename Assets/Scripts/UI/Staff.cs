using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    
    [SerializeField] private WeaponInfo weaponInfo;
    [SerializeField] private GameObject fireBallPrefab;
    [SerializeField] private Transform fireBallSpawnPoint;
    
    readonly int FIRE_HASH = Animator.StringToHash("Fire");
   
    private Animator myAnimator;


    private void Update()
    {
        //MouseFollowWithOffset();
    }
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    public void Attack()
    {
        myAnimator.SetTrigger(FIRE_HASH);
       
        GameObject newFireBall = Instantiate(fireBallPrefab, fireBallSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);

        Animator fireBallAnimator = newFireBall.GetComponent<Animator>();

        fireBallAnimator.SetBool("Travel", true);

        newFireBall.GetComponent<FireBallProjectile>().UpdateWeaponInfo(weaponInfo);
    }

    //private void MouseFollowWithOffset()
    //{
    //    Vector3 mousePos = Input.mousePosition;
    //    Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

    //    float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

    //    if (mousePos.x < playerScreenPoint.x)
    //    {
    //        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
    //    }

    //    else
    //    {

    //    }
    //    {
    //        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
    //    }
    //}

    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
}
