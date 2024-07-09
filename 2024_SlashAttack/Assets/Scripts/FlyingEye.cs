using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEye : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;

    public DetectionZone biteDetectionZone;
    public float waypointReachedDistance = 0.1f;
    public float flightSpeed = 2f;
    public Collider2D deathCollider;
    public List<Transform> waypoints;

    Transform nextWayPoint;
    int waypointNum = 0;


    public bool _hasTarget = false;

    public bool HasTarget
    {
        get
        {
            return _hasTarget;
        }

        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }

    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }




    private void Start()
    {
        nextWayPoint = waypoints[waypointNum];
    }

    //private void OnEnable()
    //{
    //    damageable.damageableDeath += OnDeath();
    //}

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }



    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;//not moving
            }
        }
    }

    private void Flight()
    {
        //fly to the next way point
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;

        //check if we have reached the waypoint already
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);

        rb.velocity = directionToWayPoint * flightSpeed;
        UpdateDirection();

        //see if we need to switch waypoints
        if (distance <= waypointReachedDistance)
        {
            //switch to next waypoint
            waypointNum++;

            if (waypointNum >= waypoints.Count)
            {
                //loop back to original waypoint        
                waypointNum = 0;
            }

            nextWayPoint = waypoints[waypointNum];//update waypoint transform
        }

    }

    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            //facing right
            if (rb.velocity.x < 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            //facing left
            if (rb.velocity.x > 0)
            {
                //flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }

    }

    public void OnDeath()
    {
        //if it dead it falls to the ground
        rb.gravityScale = 2f;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deathCollider.enabled = true;
    }

}