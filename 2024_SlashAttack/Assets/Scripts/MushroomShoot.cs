using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Damageable playerDamageable;
    public MushroomFlip mushroomFlip;
    public Collider2D hitboxDetection;
    public Collider2D playerCollider;
    [SerializeField] private float fireRate = 0.6f; //1 projectile per second
    private float nextFireTime;
    public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, mushroomFlip.firePoint.position, mushroomFlip.firePoint.rotation);
    }

    public void Update()
    {
        if (hitboxDetection.IsTouching(playerCollider) && playerDamageable.IsAlive)
        {
            if (Time.time > nextFireTime)
            {
                nextFireTime = Time.time + fireRate;
                SpawnProjectile();
            }
        }
        else
        {
            nextFireTime = 0f;
        }


        if (playerDamageable.IsAlive == false)
        {
            {
                GetComponent<MushroomShoot>().enabled = false;
            }
        }


    }

}
