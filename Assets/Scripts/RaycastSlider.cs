using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastSlider : MonoBehaviour
{

    void Start()
    {
        // Debug.Log("this position is " + transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.green);
        // takes same origin as gameobject this is attached to
        // 2nd param is direction
        /// 3rd param data of what the ray hit
        /// 4th param distance that the ray should travel
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hitinfo, 20f))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.green);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        
    }
}
