using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private float destroyTimer;
    AudioSource bulletSFX;
    //
    private Animator anim;

    void Awake()
    {
        bulletSFX = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    private void Start()
    {

        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position; //position of the mouse
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2 (direction.x, direction.y).normalized * force;//normalized so that whether the mouse cursor is far or near the speed will not change
        float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;//to get a degree float(this is math stuff so is pretty hard to grasp)
        transform.rotation = Quaternion.Euler(0,0,rot + 90);//plus 90 so our bullet is straight not sideway
        if(bulletSFX)
        {
            AudioSource.PlayClipAtPoint(bulletSFX.clip, gameObject.transform.position, bulletSFX.volume);

        }
        //New destroy logic
        Destroy(gameObject, 1.2f);
        

    }

    // Update is called once per frame
    void Update()
    {
        //destroyTimer += Time.deltaTime;
        //if (destroyTimer > 1.2f)
        //{
        //    Destroy(gameObject);
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("impact");
        Destroy(gameObject,0.3f); //does work
    }
}
