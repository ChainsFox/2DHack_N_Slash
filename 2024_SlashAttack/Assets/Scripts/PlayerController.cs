using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;
using static UnityEngine.Rendering.DebugUI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirectionsPlayer), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    Rigidbody2D rb;
    Animator animator;
    Vector2 moveInput;
    public float walkSpeed = 12f;
    public float runSpeed = 18f;
    //public float airWalkSpeed = 18f;
    public float jumpImpulse = 10f;
    Damageable damageable;
    TouchingDirectionsPlayer touchingDiretionsPlayer;
    //TouchingDirections touchingDiretions;
    //DOUBLE JUMP
    public bool doubleJump;

    //new stuff(for flipping character)
    private SpriteRenderer sprite;
    //Dash
    [Header("DASH Settings")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private TrailRenderer tr;
    public Vector2 moveDirection;
    public bool isDashing;
    public bool canDash = true;
    //CROUCH
    [Header("CROUCH Settings")]
    [SerializeField] private float crouchSpeed = 10f;
    [SerializeField] private bool isCrouching;
    [SerializeField] CapsuleCollider2D playerColl;
    //[SerializeField] Collider2D crouchingColl;
    public Vector2 standingSize;
    public Vector2 crouchingSize; //in the editor
    public Vector2 standingOffset;
    public Vector2 crouchOffset;
    [SerializeField] Transform overheadCheckCollider;
    public float overheadCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    //0.1277385 -0.2145218 (standing offset)
    //0.1277385 -0.7 (crouching offset)

    //(5.8,0.23,0)
    //ABILITIES:
    public Transform firePoint;
    public GameObject waterballPrefab;
    public bool isFacingRight;
    Quaternion yrotationRight = Quaternion.Euler(0, 0f, 0);
    Quaternion yrotationLeft = Quaternion.Euler(0, 180, 0);
    Vector3 xpositionRight = new Vector3(5.8f,0.23f,0f);
    Vector3 xpositionLeft = new Vector3(-5.8f, 0.23f, 0f);




    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDiretionsPlayer = GetComponent<TouchingDirectionsPlayer>();
        //touchingDiretions = GetComponent<TouchingDirections>();
        sprite = GetComponent<SpriteRenderer>();
        damageable = GetComponent<Damageable>();
        tr = GetComponent<TrailRenderer>();
        playerColl = GetComponent<CapsuleCollider2D>();
        standingSize = playerColl.size;
        isFacingRight = true; //can remove if needed

    }
    private void Update()
    {
        ////CROUCH 
        bool overheadHit = Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer);

        if (touchingDiretionsPlayer.IsGrounded) //Changing collider size & offset position 
        {
            doubleJump = true; //DOUBLE JUMP BOOL
            //standingColl.enabled = !isCrouching;
            //lOGIC: if we are under an object, but we release crouch, it will still remain crouching
            if (!isCrouching)//bugged - cant auto release crouch after crouching through an object
            {

                if (overheadHit)
                {
                    IsCrouched = true;
                    isCrouching = true;

                }

            }


            //only work if you are already under an object at the start of the game, and then you cant crouch ever again(auto release crouch attempt failed)
            //if(isCrouching) 
            //{
            //    if(!overheadHit)
            //    {
            //        isCrouching = false;
            //    }
            //}
                if (isCrouching)
                {
                    playerColl.size = crouchingSize;
                    playerColl.offset = crouchOffset;
                }
                else if (!isCrouching)
                {
                    playerColl.size = standingSize;
                    playerColl.offset = standingOffset;
                }
        }

        //WATERBALL DIRECTIONS(firePoint direction)
        float xPos = firePoint.transform.position.x;
        float yPos = firePoint.transform.position.y;

        if (isFacingRight)
        {
            firePoint.rotation = yrotationRight;
            //firePoint.transform.position = new Vector3(5.8f, 0.23f, 0f);
        }

        else if(!isFacingRight)
        {
            firePoint.rotation = yrotationLeft;
            //firePoint.transform.position = new Vector3(-5.8f, 0.23f, 0f);

        }

    }

    private void FixedUpdate()
    {
     
        if (isDashing)
        {
            return; //if we are dashing then none of the code below is call, after we are done dashing then we can continue to move as normal
        }


        if (!damageable.LockVelocity)//if the character is not hit(=false) then we can move, if we get hit we cant update the velocity base on our input(aka you cant move)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        //new way to flip the player if i want dont want to affect the "Shooting" script
        if (rb.velocity.x < 0)
        {            
            sprite.flipX = true;
            isFacingRight = false;
        }
        if (rb.velocity.x > 0)
        {
            sprite.flipX = false;
            isFacingRight = true;
        }    


        //DASH:
        float moveX = moveInput.x;
        float moveY = moveInput.y;
        moveDirection = new Vector2(moveX, moveY).normalized;

        if (moveDirection == Vector2.zero)//if we standing still it will dash base on which direction our character is facing
        {
            if (sprite.flipX == true)
            {
                moveDirection = new Vector2(-1, 0f);
            }
            else if (sprite.flipX == false)
            {   
                moveDirection = new Vector2(1, 0f);
            }

        }

        //up and down dash reduce(if we ever decided that the up and down dash is too much, this is one way of reducing it)
        //if(moveDirection.x == 0f && (moveDirection.y == 1f || moveDirection.y == -1f))
        //{
        //    rb.drag = 2f;
        //    dashSpeed = 30f;
        //}
        //else
        //{
        //    dashSpeed = 30f;
        //}



        ////CROUCH 

        //bool overheadHit = Physics2D.OverlapCircle(overheadCheckCollider.position, overheadCheckRadius, groundLayer);

        //if (touchingDiretionsPlayer.IsGrounded) //Changing collider size & offset position 
        //{
        //    //standingColl.enabled = !isCrouching;
        //    //lOGIC: if we are under an object, but we release crouch, it will still remain crouching
        //    if (!isCrouching)//bugged - cant auto release crouch after crouching through an object
        //    {

        //        if (overheadHit)
        //        {
        //            isCrouching = true;

        //        }

        //    }


        //    //only work if you are already under an object at the start of the game, and then you cant crouch ever again(auto release crouch attempt failed)
        //    //if(isCrouching) 
        //    //{
        //    //    if(!overheadHit)
        //    //    {
        //    //        isCrouching = false;
        //    //    }
        //    //}

        //    if (isCrouching)
        //    {
        //        playerColl.size = crouchingSize;
        //        playerColl.offset = crouchOffset;
        //    }
        //    else if(!isCrouching)
        //    {
        //        playerColl.size = standingSize;
        //        playerColl.offset = standingOffset;
        //    }
        //}




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

                if (IsCrouched)
                {
                    return crouchSpeed;//test
                }

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

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
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


    //CROUCH
    [SerializeField]
    private bool _isCrouched = false;

    public bool IsCrouched
    {
        get
        {
            return _isCrouched;

        }
        set
        {
            _isCrouched = value;
            animator.SetBool(AnimationStrings.isCrouched, value);

        }


    }


    //BUGGED: if you hold w first, then hold a or d, it will run in place again
    //public void OnLookUp(InputAction.CallbackContext context)
    //{
    //    if (!touchingDiretionsPlayer.IsGrounded)
    //    {
    //        animator.SetBool(AnimationStrings.isLooking, false);
    //        animator.SetBool(AnimationStrings.lockVelocity, false);

    //    }
    //    if (IsMoving)
    //    {
    //        animator.SetBool(AnimationStrings.isLooking, false);
    //        animator.SetBool(AnimationStrings.lockVelocity, false);
    //    }
    //    if (touchingDiretionsPlayer.IsGrounded && !IsMoving)
    //    {

    //        if (context.started)
    //        {   
    //            animator.SetBool(AnimationStrings.isLooking, true);
    //            animator.SetBool(AnimationStrings.lockVelocity, true);
    //            animator.SetBool(AnimationStrings.isMoving, false);
    //        }
    //        if (context.canceled)
    //        {
    //            animator.SetBool(AnimationStrings.isLooking, false);
    //            animator.SetBool(AnimationStrings.lockVelocity, false);
    //        }

    //    }
    //}
    public void OnCrouch(InputAction.CallbackContext context)
    {
            if (context.started)
            {
                isCrouching = true;
                IsCrouched = true;
            }
            else if (context.canceled)
            {
                isCrouching = false;
                IsCrouched = false;
            }

    }

    public void OnMove(InputAction.CallbackContext context)
    {

        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            //IsMoving = moveInput != Vector2.zero;
            IsMoving = moveInput.x != 0f; //for fixing bug running in place

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
        isCrouching = false;
        IsCrouched = false; 
        if (context.started && IsAlive && touchingDiretionsPlayer.IsGrounded)/*&& CanMove */
        {
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
        if(context.started && doubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            doubleJump = false;
        }


    }

    public void OnHit(int damge, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        isCrouching = false;
        IsCrouched = false;
        if (context.started && canDash)
        {
            StartCoroutine(Dash());
        }
        //StartCoroutine(Dash());

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        tr.emitting = true;
        yield return new WaitForSeconds(dashDuration);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void WaterBall(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Instantiate(waterballPrefab, firePoint.position, firePoint.rotation); 
        }


    }



}
