using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(Animator))]

public class PlayerController : MonoBehaviour
{

    

    [Header("Movement")]
    [SerializeField] float speed = 6f;
    [SerializeField] float smoothturn = 0.1f;


    [Header("Leeway Settings")]
    [SerializeField] float coyoteTime = 0.1f; // How long the grace period lasts
    private float coyoteTimeCounter;


    float turnsmoothVelocity;

    [Header("pysics")]
    [SerializeField] float gravity = -19.62f; // Double Earth gravity feels better in games
    [SerializeField] float jumpHeight = 2f;


    private Vector3 playerVelocity; // Tracks vertical speed (gravity/jumping)
    private bool isGrounded;

    [Header("Camera")]

    public Transform cam;


    public CharacterController controller;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private Vector2 lookInput;


    // anamator

    Animator animator;
    //hashed animations 
    private static readonly int IsWalkingHash = Animator.StringToHash("isWalking");
    private static readonly int IsJumpingHash = Animator.StringToHash("isJumping");


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();

        // Get actions directly from the PlayerInput component
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
    }




    private void Update() {
        isGrounded = controller.isGrounded;
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            playerVelocity.y = -2f;
        }
        else { 
            coyoteTimeCounter -= Time.deltaTime;
        }

        Move();
        ApplyGravityAndJump();
    }

    private void Move() {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0f, input.y).normalized;

        if (move.magnitude >= 0.1f) {
            animator.SetBool(IsWalkingHash, true);
            float targAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targAngle, ref turnsmoothVelocity, smoothturn);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 movedir = Quaternion.Euler(0, targAngle, 0) * Vector3.forward;

            controller.Move(movedir.normalized * speed * Time.deltaTime);
        }
        else {
            animator.SetBool(IsWalkingHash, false);
        }
        
    }

    private void ApplyGravityAndJump()
    {

        if (jumpAction.triggered && coyoteTimeCounter > 0f)
        {

            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimeCounter = 0f;
        }
        if (!isGrounded) {
            animator.SetBool(IsJumpingHash, true);
            
        } else {
            animator.SetBool(IsJumpingHash, false);
        }


            playerVelocity.y += gravity * Time.deltaTime;

        controller.Move(playerVelocity * Time.deltaTime);
    }
}
