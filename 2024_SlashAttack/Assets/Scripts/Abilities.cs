using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    TouchingDirectionsPlayer touchingDiretionsPlayer;
    PlayerController playerController;
    Animator animator;
    //ABILITIES:
    public Transform firePoint;
    public Transform fireStylePoint;
    public GameObject waterballPrefab;
    public GameObject lightningStrikePrefab;
    public GameObject fireBreathPrefab;
    private Rigidbody2D rb;
    public GameObject fireBreathInstance;
    public float fireBreathTimer = 0f;

    //(old logic) - now flip with rotate 180
    //Quaternion yrotationRight = Quaternion.Euler(0, 0f, 0);
    //Quaternion yrotationLeft = Quaternion.Euler(0, 180, 0);
    //Vector3 xpositionRight = new Vector3(5.8f,0.23f,0f);
    //Vector3 xpositionLeft = new Vector3(-5.8f, 0.23f, 0f);

    [Header("ABILITY INFO/COOLDOWNS")]
    //ABILITY 1:
    public Image abilityImage1;
    public TMP_Text abilityText1;
    [SerializeField] private float ability1Cooldown = 5f;
    private bool isAbility1Cooldown = false;
    private float currentAbility1Cooldown;

    //ABILITY 2:
    public Image abilityImage2;
    public TMP_Text abilityText2;
    [SerializeField] private float ability2Cooldown = 1f;
    public bool isAbility2Cooldown = false;
    public float currentAbility2Cooldown;

    //ABILITY 3:
    public Image abilityImage3;
    public TMP_Text abilityText3;
    [SerializeField] private float ability3Cooldown = 2f;
    public bool isAbility3Cooldown = false;
    public float currentAbility3Cooldown;
    public bool holdFlame = false;
    private float progressFlame = 0f;
    public float progressFlameFillSpeed = 0.34f;//3 second fire breath
    public Slider progressFlameSlider;

    //ABILITY 4:
    public Image abilityImage4;
    public TMP_Text abilityText4;
    public bool isAbility4Cooldown = false;
    public float currentAbility4Cooldown;

    //ABILITY 5:
    public Image abilityImage5;
    public TMP_Text abilityText5;
    public bool isAbility5Cooldown = false;
    public float currentAbility5Cooldown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        touchingDiretionsPlayer = GetComponent<TouchingDirectionsPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //default:
        abilityImage1.fillAmount = 0;
        abilityText1.text = "";

        abilityImage2.fillAmount = 0;
        abilityText2.text = "";

        abilityImage3.fillAmount = 0;
        abilityText3.text = "";

        abilityImage4.fillAmount = 0;
        abilityText4.text = "";

        abilityImage5.fillAmount = 0;
        abilityText5.text = "";

    }

    private void Update()
    {
        AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
        AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);
        AbilityCooldown(ref currentAbility3Cooldown, ability3Cooldown, ref isAbility3Cooldown, abilityImage3, abilityText3);
        AbilityCooldown(ref currentAbility4Cooldown, playerController.dashCooldown, ref isAbility4Cooldown, abilityImage4, abilityText4);
        AbilityCooldown(ref currentAbility5Cooldown, playerController.slideCooldown, ref isAbility5Cooldown, abilityImage5, abilityText5);
        progressFlameSlider.value = progressFlame;
        //Ability 3 logic:
        if (holdFlame)
        {
            fireBreathTimer += Time.deltaTime;
            progressFlame += progressFlameFillSpeed * Time.deltaTime;
        }
        if (holdFlame && !isAbility3Cooldown)
        {

            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            playerController.enabled = false;
            //spawnFireBreath();
            Invoke(nameof(spawnFireBreath), 0.35f);
            animator.SetTrigger(AnimationStrings.usingAbility3);
            animator.SetBool(AnimationStrings.holdFlame, true);
            //ability 3 cooldown:
            isAbility3Cooldown = true;
            currentAbility3Cooldown = ability3Cooldown;


        }
        if (!holdFlame || fireBreathTimer >= 3f)
        {           
            playerController.enabled = true;
            animator.SetBool(AnimationStrings.holdFlame, false);
            Destroy(fireBreathInstance);
            fireBreathTimer = 0f;            
            rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            progressFlame = 0f;
            holdFlame = false;//bruh
        }


        




    }

    public void SpawnWaterBall()
    {
        Instantiate(waterballPrefab, firePoint.position, firePoint.rotation);
    }

    public void SpawnLightningStrike()
    {
        Instantiate(lightningStrikePrefab, firePoint.position, firePoint.rotation);
    }

    public void spawnFireBreath()
    {
        fireBreathInstance = Instantiate(fireBreathPrefab, fireStylePoint.position, fireStylePoint.rotation);
    }


    //ability 1 logic:
    public void WaterBall(InputAction.CallbackContext context)
    {
        if (context.started && playerController.IsAlive && touchingDiretionsPlayer.IsGrounded && !isAbility1Cooldown && !playerController.isSliding)
        {
            freezePlayer();
            animator.SetTrigger(AnimationStrings.usingAbility);
            Invoke(nameof(SpawnWaterBall), 0.4f);
            Invoke(nameof(unfreezePlayer), 0.4f);
            //ability 1 cooldown:
            isAbility1Cooldown = true;
            currentAbility1Cooldown = ability1Cooldown;
            Destroy(fireBreathInstance);

        }

    }


    public void LightningStrike(InputAction.CallbackContext context)
    {
        if (context.started && playerController.IsAlive && touchingDiretionsPlayer.IsGrounded && !isAbility2Cooldown && !playerController.isSliding)
        {
            freezePlayer();
            animator.SetTrigger(AnimationStrings.usingAbility2);
            Invoke(nameof(SpawnLightningStrike), 0.3f);
            Invoke(nameof(unfreezePlayer), 0.4f);
            //ability 2 cooldown:
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;
            Destroy(fireBreathInstance);



        }


    }


    public void FireBreath(InputAction.CallbackContext context)
    {
        if (playerController.IsAlive && touchingDiretionsPlayer.IsGrounded && !playerController.isSliding)
        {
            if (context.started && !isAbility3Cooldown)
            {
                holdFlame = true;
            }
            if (context.canceled)
            {
                holdFlame = false;
            }



        }
    }







    public void freezePlayer()
    {
        rb.drag = 1000f;
        rb.gravityScale = 1000f;
        //rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void unfreezePlayer()
    {
        rb.drag = 0.05f;
        rb.gravityScale = 4.2f;
        //rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }



    private void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, TMP_Text skillText)
    {
        if(isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if(currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if(skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if(skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if(skillImage != null)
                {
                    skillImage.fillAmount = currentCooldown / maxCooldown;//fill in slowly
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();//round up the number to show on text
                }

            }



        }

    }





}
