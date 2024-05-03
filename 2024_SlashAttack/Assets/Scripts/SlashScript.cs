using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    private float destroyTimer;
    private Collider2D coll;

    // Start is called before the first frame update
    private void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position; //position of the mouse
        Vector3 rotation = transform.position - mousePos;//normalized so that whether the mouse cursor is far or near the speed will not change
        float rot = Mathf.Atan2(rotation.x, rotation.y) * Mathf.Rad2Deg;//to get a degree float(this is math stuff so is pretty hard to grasp)
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);//plus 90 so our bullet is straight not sideway
        //
        coll = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer += Time.deltaTime;
        if (destroyTimer > 0.35f)
        {
            Destroy(gameObject);
            Destroy(coll);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //does work
    }
}
