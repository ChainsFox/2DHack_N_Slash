using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    public GameObject childObject;
    public Rigidbody2D rb;

    Animator animator;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);
            //if health drop below or = to 0, the character is no longer alive
            if (_health <= 0)
            {
                IsAlive = false;
            }   
        }
    }
    

    //public bool IsHit { get
    //    {
    //        return animator.GetBool(AnimationStrings.isHit);   
    //    }

    //    private set
    //    {
    //        animator.SetBool(AnimationStrings.isHit, value);
    //    }


    //} //remove in part 16

    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set " + value);
            if (value == false)
            {
                damageableDeath.Invoke();
                childObject.SetActive(false);
                //test(worked)-19/06/2024-fixed bug where you still move when death
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX;

            }

        }

    }

    //the velocity should not be changed while this is true but needs to be respected by other physics components like the player controller
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            if (timeSinceHit > invincibilityTime)
            {
                //remove invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }

        //Hit(10,yeh);
    }
    //test:
    //private Vector2 yeh = new Vector2(0,0);
    
    //return the damageable took damage or not
    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)//check if the character IsAlive and if the character is not in Invincible state
        {
            Health -= damage;
            isInvincible = true;

            //notify other subscribed components that the damageable was hit to handle the knockback and such
            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);//this is a unity event to pass that knockback information to another script
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    //return whether the character was heal or not
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            //Mathf.Max return the higher value out of the 2 values. Logic: if the health is >= max health, then this is gonna return a negative value.
            //In that case we are gonna default the max heal to 0, because we can't heal above our max health.
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);//Mathf.Min return the lower value out of the 2, between the maxium that we can heal, which is 20, and the amount of heal that is low or enough to heal us. 
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);//get the ui manager to create the healing text!!!
            return true;
        }

        return false;
    }



}