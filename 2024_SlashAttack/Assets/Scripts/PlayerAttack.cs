using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.U2D;

public class PlayerAttack : MonoBehaviour
{
    //Collider2D attackCollider;//can be remove part 16
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    //public Vector2 flipxKnockback = Vector2.zero;

    //private void Awake()
    //{
    //    attackCollider = GetComponent<Collider2D>();//can be remove part 16

    //}



    private void OnTriggerEnter2D(Collider2D collision)
    {
        //see if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();


        if (damageable != null)
        {
            //this is for flipping the knocback depend on whether we are facing the left or right
            //if x > 0 then apply knock back as normal, if not(then x is < 0) we apply the same force but reverse 
            //Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
        

            //hit the target
            bool gotHit = damageable.Hit(attackDamage, knockback);

            //Debug.Log
            //if (gotHit)
            //    Debug.Log(collision.name + " hit for " + attackDamage);
        }

    }
}