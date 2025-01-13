using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomFlip : MonoBehaviour
{
    public Transform firePoint;
    public Knight Knight;
    public Rigidbody2D rb;

    private void FixedUpdate()
    {
        if (rb.velocity.x < 0 && Knight.isFacingRight)
        {
            FlipFire();
        }
        if (rb.velocity.x > 0 && !Knight.isFacingRight)
        {
            FlipFire();
        }

    }


    public void FlipFire()
    {
        Knight.isFacingRight = !Knight.isFacingRight;
        firePoint.transform.Rotate(0f, 180f, 0f);
    }



}
