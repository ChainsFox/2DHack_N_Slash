using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirectionsPlayer), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb;
    Animator animator;
    [Header("Horizontal Movement")]
    Vector2 moveInput;
    public float walkSpeed = 12f;
    public float runSpeed = 18f;
    //public float airWalkSpeed = 18f;
    public float jumpImpulse = 10f;
    Damageable damageable;
    //new stuff
    private SpriteRenderer sprite;

    TouchingDirectionsPlayer touchingDiretionsPlayer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDiretionsPlayer = GetComponent<TouchingDirectionsPlayer>();
        sprite = GetComponent<SpriteRenderer>();
        damageable = GetComponent<Damageable>();
    }

    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)//if the character is not hit(=false) then we can move, if we get hit we cant update the velocity base on our input(aka you cant move)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
        //sprite.flipX = rb.velocity.x < 0f; //new way to flip the player if i want dont want to affect the "Shooting" script
        if(rb.velocity.x < 0)
        {
            sprite.flipX = true;
        }
        if(rb.velocity.x > 0) 
        {
            sprite.flipX = false;
        }

    }

    //public bool _isFacingRight = true;

    //public bool IsFacingRight
    //{
    //    get { return _isFacingRight; }
    //    private set
    //    {
    //        if (_isFacingRight != value)
    //        {
    //            //flip the local scale to make the player face the opposite direction
    //            transform.localScale *= new Vector2(-1, 1);
                 

    //        }
    //        _isFacingRight = value;

    //    }
    //}

    //private void SetFacingDirection(Vector2 moveInput)
    //{
    //    if (moveInput.x > 0 && !IsFacingRight)
    //    {
    //        //face right
    //        IsFacingRight = true;
    //    }
    //    else if (moveInput.x < 0 && IsFacingRight)
    //    {
    //        //face left
    //        IsFacingRight = false;
    //    }
    //}

    public float CurrentMoveSpeed
    {
        get
        {
                if (IsMoving /*&& !touchingDiretions.IsOnWall*/) //03/05/2024-NEW: The player can freely move fast on the ground or on the air
                {
                    //if (touchingDiretions.IsGrounded)
                    //{
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    //}
                    //else
                    //{//Air move
                    //    return airWalkSpeed;
                    //}

                }
                else
                {
                    //idle speed = 0;
                    return 0;
                }





        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;


        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);



        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;

        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);

        }


    }

    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            //SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;

        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        //TODO: Check if alive as well
        if (context.started && touchingDiretionsPlayer.IsGrounded)/*&& CanMove */
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }

    }

    public void OnHit(int damge, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }




}
