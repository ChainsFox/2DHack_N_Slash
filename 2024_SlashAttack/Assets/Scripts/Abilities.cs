using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    private Rigidbody2D rb;
    //(old logic) - now flip with rotate 180
    //Quaternion yrotationRight = Quaternion.Euler(0, 0f, 0);
    //Quaternion yrotationLeft = Quaternion.Euler(0, 180, 0);
    //Vector3 xpositionRight = new Vector3(5.8f,0.23f,0f);
    //Vector3 xpositionLeft = new Vector3(-5.8f, 0.23f, 0f);

    [Header("Horizontal Movement")]
    public Image abilityImage1;
    public TMP_Text abilityText1;
    //ABILITY 1:
    [SerializeField] private float ability1Cooldown = 5f;
    private bool isAbility1Cooldown = false;
    private float currentAbility1Cooldown;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        touchingDiretionsPlayer = GetComponent<TouchingDirectionsPlayer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        abilityImage1.fillAmount = 0;

        abilityText1.text = "";

    }

    private void Update()
    {
        AbilityCooldown(ref currentAbility1Cooldown, ability1Cooldown, ref isAbility1Cooldown, abilityImage1, abilityText1);
    }


    public void WaterBall(InputAction.CallbackContext context)
    {
        if (context.started && playerController.IsAlive && touchingDiretionsPlayer.IsGrounded && !isAbility1Cooldown)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
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
                    skillImage.fillAmount = currentCooldown / maxCooldown;
                }
                if (skillText != null)
                {
                    skillText.text = Mathf.Ceil(currentCooldown).ToString();
                }

            }



        }

    }





}
