using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Shooting : MonoBehaviour //attack script
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject bullet;
    public GameObject slash;
    public Transform bulltetTransform;
    public bool canFire;
    private float timer;
    public float timeBetweenFiring; //0.3f
    //
    public bool holdAttack = false;
    public bool holdShoot = false;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //UPGRADE: Remember to try to update this to new input system
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition); //transform mouse position to a vector 3 variable

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if(holdAttack==true && canFire)
        {
            canFire = false;
            Instantiate(slash, bulltetTransform.position, Quaternion.identity);
        }

        if (holdShoot == true && canFire)
        {
            canFire = false;
            Instantiate(bullet, bulltetTransform.position, Quaternion.identity);
        }



        //OLD INPUT SYSTEM:
        //if (Input.GetMouseButton(0) && canFire)
        //{
        //    canFire = false;
        //    Instantiate(bullet, bulltetTransform.position, Quaternion.identity);
        //}
        //else if(Input.GetMouseButton(1) && canFire)
        //{
        //    canFire = false;
        //    Instantiate(slash, bulltetTransform.position, Quaternion.identity);
        //} 


    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            holdAttack=true;

        }
        if(context.canceled)
        {
            holdAttack=false;
        }
   
        
    }



    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            holdShoot = true;

        }
        if (context.canceled)
        {
            holdShoot = false;
        }

    }


}
