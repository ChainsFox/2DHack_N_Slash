using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public DetectionZone zone;

    public void SpawnProjectile()
    {
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }

    //private void Update()
    //{
    //    if(zone.detectedColliders != null)
    //    {
    //        Invoke(nameof(SpawnProjectile), 1f);
    //    }
    //}

}
