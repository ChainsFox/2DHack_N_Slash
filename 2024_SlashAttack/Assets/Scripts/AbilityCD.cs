using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "new ability cd", menuName = "ability cd")]
public class AbilityCD : ScriptableObject
{
    [Header("ABILITY INFO/COOLDOWNS")]
    public Image abilityImage;
    public TMP_Text abilityText;
    public float abilityCooldown;
    //[SerializeField] private float abilityCooldown;
    public bool isAbilityCooldown = false;
    public float currentAbilityCooldown;

    public void StartCD(ref Image abilityImage, ref TMP_Text abilityText)
    {
        abilityImage.fillAmount = 0;
        abilityText.text = "";

    }

    public void AbilityCooldown(ref float currentCooldown, float maxCooldown, ref bool isCooldown, Image skillImage, TMP_Text skillText)
    {
        if (isCooldown)
        {
            currentCooldown -= Time.deltaTime;

            if (currentCooldown <= 0f)
            {
                isCooldown = false;
                currentCooldown = 0f;

                if (skillImage != null)
                {
                    skillImage.fillAmount = 0f;
                }
                if (skillText != null)
                {
                    skillText.text = "";
                }
            }
            else
            {
                if (skillImage != null)
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
