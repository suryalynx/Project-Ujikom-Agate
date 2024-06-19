using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Weapon")]
    public GameObject weaponPrefabs;
    private PlayerInputActions inputActions;

    void Awake(){
        inputActions = GetComponent<PlayerInputActions>();
    }
    void OnEnable()
    {
        inputActions.Player.Fire.Enable();
        inputActions.Player.Fire.performed += ctx => Attack();
    }
    void OnDisable()
    {
        inputActions.Player.Fire.performed -= ctx => Attack();
        inputActions.Player.Fire.Disable();
    }

    private void Attack()
    {
        Debug.Log("Player Attack");
        GameObject weapon = Instantiate(weaponPrefabs,transform.position + transform.forward, transform.rotation);
        Rigidbody rb = weapon.GetComponent<Rigidbody>();

        if(rb != null){
            rb.AddForce(transform.forward * 10f, ForceMode.Impulse);
        }
    }
}
