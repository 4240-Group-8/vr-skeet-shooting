using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowPlayer : MonoBehaviour
{
    public Transform hoverPos; // A hovering follow target under the player 
                               // that this Ui will follow
    public float followSpeed = 3;

    void Start()
    {
        // initialOffset = player.transform.position - transform.position
        
    }
    /// Attach this script to the Ui which is following the player
    void LateUpdate()
    {
        GetComponent<Rigidbody>().velocity = followSpeed 
                * (hoverPos.position - transform.position);
    }

}
