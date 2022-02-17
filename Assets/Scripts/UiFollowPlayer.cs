using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiFollowPlayer : MonoBehaviour
{
    public Transform hoverPos; // A hovering follow target under the player 
                               // that this Ui will follow
    public float followSpeed = 3;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // initialOffset = player.transform.position - transform.position
        
    }
    /// Attach this script to the Ui which is following the player
    void Update()
    {
        // GetComponent<Rigidbody>().velocity = followSpeed 
                // * (hoverPos.position - transform.position);
        transform.position = Vector3.SmoothDamp(transform.position, 
                                                hoverPos.position, 
                                                ref velocity,
                                                smoothTime);
    }

}
