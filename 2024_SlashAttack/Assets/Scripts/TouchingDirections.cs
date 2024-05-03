using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirections : MonoBehaviour
{
    Rigidbody2D rb;
    public ContactFilter2D castFilter;
    public float groundDistance = 0.05f;
    [SerializeField] private LayerMask jumpableGround;
    //public float wallDistance = 0.2f;
    //public float ceilingDistance = 0.05f;

    CapsuleCollider2D touchingCol;
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    //RaycastHit2D[] wallHits = new RaycastHit2D[5];
    //RaycastHit2D[] ceilingHits = new RaycastHit2D[5];

    Animator animator;

    [SerializeField] //so you can see it in the inspector
    private bool _isGrounded = true;
    public bool IsGrounded
    {
        get
        {
            return _isGrounded;
        }
        private set
        {
            _isGrounded = value;
            animator.SetBool(AnimationStrings.isGrounded, value);
        }

    }

    //[SerializeField]
    //private bool _isOnWall;
    //public bool IsOnWall
    //{
    //    get
    //    {
    //        return _isOnWall;
    //    }
    //    private set
    //    {
    //        _isOnWall = value;
    //        animator.SetBool(AnimationStrings.isOnWall, value);
    //    }

    //}

    //[SerializeField]
    //private bool _isOnCeiling;
    ////private Vector2 wallCheckDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left; //this no longer work for the flip method
    //public bool IsOnCeiling
    //{
    //    get
    //    {
    //        return _isOnCeiling;
    //    }
    //    private set
    //    {
    //        _isOnCeiling = value;
    //        animator.SetBool(AnimationStrings.isOnCeiling, value);
    //    }

    //}



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingCol = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() //anything related to physics we want to put it in the "fixed update"
    {
        //IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0; 
        IsGrounded = Physics2D.BoxCast(touchingCol.bounds.center, touchingCol.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        //IsOnWall = touchingCol.Cast(wallCheckDirection, castFilter, wallHits, wallDistance) > 0;
        //IsOnCeiling = touchingCol.Cast(Vector2.up, castFilter, ceilingHits, ceilingDistance) > 0;
        //explain: Cast function will store the result in the groundHits array, it will also return an int which is the number of collision that this cast detected,
        //so if the int return is greater than zero, that means that it detected a collision on the ground.
    }

    //public bool IsGrounded()
    //{
    //    return Physics2D.BoxCast(touchingCol.bounds.center, touchingCol.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    //    // this code is like we creating a box around our player the same size in the editor, and we check if the box is overlaping with the ground. Return true or false
    //    // Choose layer mask to check if it is grounded.
    //}


}