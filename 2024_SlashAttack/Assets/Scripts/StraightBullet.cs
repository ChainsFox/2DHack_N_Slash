using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightBullet : MonoBehaviour
{

    [SerializeField] public float speed = 10f;
    public Rigidbody2D rb;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 2.5f);
    }

    
}
