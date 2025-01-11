using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Damageable damageable;
    public Transform firePoint;
    public Collider2D hitboxDetection;
    public Collider2D playerCollider;
    private float timer;
    [SerializeField] private float delay =0.8f;
    public Knight Knight;
    public Rigidbody2D rb;
    //[SerializeField] public float range = 13f;
    public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    private void Update()
    {
        //float distance = Vector2.Distance(transform.position, Player.transform.position); 
        //distance < range
        //zone.detectedColliders != null
        if (hitboxDetection.IsTouching(playerCollider) && damageable.IsAlive == true)
        {
            timer += Time.deltaTime;
            //Invoke(nameof(SpawnProjectile), 0f);

            if (timer >= delay)
            {
                timer = 0;
                //anim.SetBool("IsAttacking", true);
                InvokeRepeating(nameof(SpawnProjectile), 0f, 0f);
            }
        }
        if(damageable.IsAlive == false) 
        {
            GetComponent<ProjectileAttack>().enabled = false;
        }

    }

    private void FixedUpdate()
    {
        if (rb.velocity.x < 0 && Knight.isFacingRight)
        {
            FlipSide();
        }
        if (rb.velocity.x > 0 && !Knight.isFacingRight)
        {
            FlipSide();
        }

    }

    public void FlipSide()
    {
        Knight.isFacingRight = !Knight.isFacingRight;
        firePoint.transform.Rotate(0f, 180f, 0f);
    }



}
