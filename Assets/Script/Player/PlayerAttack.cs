using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject weaponPrefabs;
    public Transform projectile;

    private Animator animator;
    private bool weaponFired;
    private bool fireInput;
    private PlayerInputActions inputActions;

    void Awake()
    {
        animator = GetComponent<Animator>();
        inputActions = new PlayerInputActions();
        

        inputActions.Player.Fire.performed += ctx => fireInput = ctx.ReadValueAsButton();
        inputActions.Player.Fire.canceled += ctx => fireInput = false;
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (!weaponFired)
        {
            {
                if (fireInput)
                {
                    weaponFired = true;
                    GameObject weapon = Instantiate(weaponPrefabs, projectile.position + projectile.forward, projectile.rotation);
                    Rigidbody rb = weapon.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        rb.AddForce(projectile.forward * 40f, ForceMode.Impulse);
                    }
                    SFXManager.instance.PlaySFX("SFXFire");
                    animator.SetTrigger("Throw");
                    Destroy(weapon, 3f);

                }
            }
        }
        else if (weaponFired)
        {
            fireInput = false;
            weaponFired = false;
        }

    }
}
