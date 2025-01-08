using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public Collider2D hitboxDetection;
    public Collider2D playerCollider;
    private float timer;
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
        if (rb.velocity.x < 0 && Knight.isFacingRight)
        {
            FlipSide();
        }
        if (rb.velocity.x > 0 && !Knight.isFacingRight)
        {
            FlipSide();
        }

        //zone.detectedColliders != null
        if (hitboxDetection.IsTouching(playerCollider))
        {
            timer += Time.deltaTime;
            //Invoke(nameof(SpawnProjectile), 0f);

            if (timer >= 1)
            {
                timer = 0;
                //anim.SetBool("IsAttacking", true);
                //InvokeRepeating(nameof(SpawnProjectile), 0.42f, 0f);
            }
        }
    }

    public void FlipSide()
    {
        Knight.isFacingRight = !Knight.isFacingRight;
        firePoint.transform.Rotate(0f, 180f, 0f);
    }



}
