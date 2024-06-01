using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{   

    public Camera cam;
    public Transform followTarget;

    //starting position for the parallax game object
    Vector2 startingPosition;
    // start z value of the parallax game object
    float startingZ;

    // distance that the camera has moved from the starting position of the parallax object    
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //If object is in front of target, use near clip plane. If behind target use farClipPlane
    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    // the further the object from the player, the faster the parallaxEffect object will move. Drag it's Z value closer to the target to make it move slower
    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = new Vector2(transform.position.x, transform.position.y);
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //when the target moves, move the parallax object at the same distance times a muliplier
        Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

        //the x/y position changes based on the target travel speed times the parallax factor but Z stays consistent
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
