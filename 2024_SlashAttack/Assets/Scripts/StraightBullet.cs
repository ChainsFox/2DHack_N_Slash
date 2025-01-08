using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{

    [SerializeField] public float speed = 10f;
    [SerializeField] public float delayDestroyTime = 2.5f;
    public Rigidbody2D rb;

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, delayDestroyTime);
    }

    private void OnTriggerEnter2D(Collider2D HitInfo)
    {

        Destroy(gameObject);

    }





}
