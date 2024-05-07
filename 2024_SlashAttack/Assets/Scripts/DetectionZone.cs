using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent noCollidersRemain; //name logic: its not a properties therefore we don't capitalize the first letter
    public List<Collider2D> detectedColliders = new List<Collider2D>();
    Collider2D col;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        detectedColliders.Add(collision); //when something enter the zone we want to keep track of it
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        detectedColliders.Remove(collision);//when we remove a collision from a list of colliders

        if (detectedColliders.Count <= 0)
        {
            noCollidersRemain.Invoke();
        }
    }
}