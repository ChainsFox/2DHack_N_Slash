using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;
    //starting position for the parallax gameobject
    Vector2 startingPosition;

    //start z value of the parallax gameobject
    float startingZ;

    //distance that the camera has moved from the starting position of the parallax object
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition; //use the "=>" to update itselft on every frame
    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;
    float clippingPane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    //the further the object from the player, the faster the parallax object will move, drag it's z value closer to the target to make it move slower   
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPane;


    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }


    void Update()
    {
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor; //when the target moves, move the parallax object the same distance times a mutiplier
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ); //the x/y position changes based on target travel speed times the parallax factor, but z stay consistent
    }
}