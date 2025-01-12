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
    //private float timer;
    //[SerializeField] private float delay = 0.8f;
    [SerializeField] private float fireRate = 0.8f; //1 projectile per second
    [SerializeField] private float nextFireTime = 0f;
    public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, mushroomFlip.firePoint.position, mushroomFlip.firePoint.rotation);
    }

    public void Update()
    {
        if (hitboxDetection.IsTouching(playerCollider) && playerDamageable.IsAlive)
        {
            //timer += Time.deltaTime;

            //SpawnProjectile();
            //if (timer > delay)
            //{
            //    timer = 0f;
            //    InvokeRepeating(nameof(SpawnProjectile), 0f, 0f);
            //}

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
