using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // The camera should be following the player's car 
    // This is update per frame
    // Update is called once per frame

    [SerializeField] GameObject thingToFollow;
    int zDistanceToCar = -10;
    void LateUpdate()
    {
        transform.position = thingToFollow.transform.position + new Vector3 (0, 0, zDistanceToCar) ;
    }
}


