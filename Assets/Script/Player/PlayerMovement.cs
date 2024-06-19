using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 350;

    private Rigidbody rb;
    private Animator animator;
    private Vector2 moveInput;
    private PlayerInputActions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInputActions();

        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
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
        Movement();
    }

    private void Movement()
    {
        Vector3 move = new Vector3(moveInput.x * speed, rb.velocity.y);
        rb.velocity = move;

        if(moveInput.x > 0){
            animator.SetBool("Right", true);
        }
        else if(moveInput.x <0){
            animator.SetBool("Left", true);
        }
    }
}
