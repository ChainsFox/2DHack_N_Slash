using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public GameObject waterballPrefab;
    public GameObject lightningStrikePrefab;
    private Rigidbody2D rb;
    public float timer = 0f;
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

    }

    private void Update()
    {
        AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
        AbilityCooldown(ref currentAbility2Cooldown, ability2Cooldown, ref isAbility2Cooldown, abilityImage2, abilityText2);

    }

    //ability 1 logic:
    public void WaterBall(InputAction.CallbackContext context)
    {
        if (context.started && playerController.IsAlive && touchingDiretionsPlayer.IsGrounded && !isAbility1Cooldown)
        {
            freezePlayer();
            animator.SetTrigger(AnimationStrings.usingAbility);
            Invoke(nameof(SpawnWaterBall), 0.4f);
            //ability 1 cooldown:
            isAbility1Cooldown = true;
            currentAbility1Cooldown = ability1Cooldown; 


        }

    }

    public void SpawnWaterBall()
    {
        Instantiate(waterballPrefab, firePoint.position, firePoint.rotation);
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
    }

    public void SpawnLightningStrike()
    {
        Instantiate(lightningStrikePrefab, firePoint.position, firePoint.rotation);
    }



    public void LightningStrike(InputAction.CallbackContext context)
    {
        if (context.started && playerController.IsAlive && touchingDiretionsPlayer.IsGrounded && !isAbility2Cooldown)
        {
            timer += Time.deltaTime;
            freezePlayer();
            //rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            animator.SetTrigger(AnimationStrings.usingAbility2);
            //ability 2 cooldown:
            isAbility2Cooldown = true;
            currentAbility2Cooldown = ability2Cooldown;
            Invoke(nameof(SpawnLightningStrike),0.3f);
            //Instantiate(lightningStrikePrefab, firePoint.position, firePoint.rotation);
            //Destroy(lightningStrikePrefab, 0.4f);

            Invoke(nameof(unfreezePlayer), 0.4f);



        }

    }

    public void freezePlayer()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
    }

    public void unfreezePlayer()
    {
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
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
