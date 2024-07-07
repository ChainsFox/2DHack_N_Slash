using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //health pickup script: detect that our damageable character walk into its box collider zone and then we gonna call the heal funciton
    //on the damageable component. In Damageable script, when the damageable component get healed we invoke the event "characterHealed" from 
    //the script CharacterEvent, which mean any other component that have a function subcribed to the character healed event are gonna
    //run that function with the parameter in the characterHealed event(gameObject, healthRestore).
    public int healthRestore = 20;
    public Vector3 spinRotaionSpeed = new Vector3(0, 180, 0);
    AudioSource pickupSource;

    void Awake()
    {
        pickupSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();//health logic

        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                if (pickupSource)
                    AudioSource.PlayClipAtPoint(pickupSource.clip, gameObject.transform.position, pickupSource.volume);

                Destroy(gameObject);//use once and destroy
            }

        }
    }

    private void Update()
    {
        transform.eulerAngles += spinRotaionSpeed * Time.deltaTime;//just to rotate the object
    }


}