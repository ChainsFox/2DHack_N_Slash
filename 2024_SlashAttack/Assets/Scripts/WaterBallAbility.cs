using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu]
public class WaterBallAbility : Ability
{
    [SerializeField] public float speed = 10f;
    //public Rigidbody2D rb;
    public GameObject BulletPrefab;


    public override void Activate(GameObject parent)
    {
        //ability logic:
        Instantiate(BulletPrefab);
        


    }

    public override void BeginCooldown(GameObject parent)
    {
        //reset stuff to its normal value
    }
}
