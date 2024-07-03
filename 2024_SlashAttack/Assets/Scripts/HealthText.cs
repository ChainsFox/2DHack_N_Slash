using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    //pixels per second
    public Vector3 moveSpeed = new Vector3(0, 75, 0); //apply to rectransform because this is a UI element
    public float timeToFade = 1f;

    RectTransform textTransform;

    TextMeshProUGUI textMeshPro;

    private float timeElapsed; //thoi gian troi qua
    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;//starting value
    }

    private void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime; //we are applying the movespeed to the position on every second
        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)//thoi gian troi qua be hon thoi gian fade.
        {
            float fadeAlpha = startColor.a * (1 - timeElapsed / timeToFade); //RGBA, rgb = color, a = transparency
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);//updated value, it start with the default value and then start to fade
        }
        else
        {
            Destroy(gameObject);//destroy this "gameObject"
        }
    }

}