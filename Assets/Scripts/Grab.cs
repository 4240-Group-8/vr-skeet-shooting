using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    public OVRInput.Controller Controller; // L or R
    public string buttonName;
    public float grabRadius; // range of sphere cast
    public LayerMask grabMask; // only obj in this layer can be grabbed

    private GameObject grabbedObject; // obj being held
    private bool grabbing; 
    // Start is called before the first frame update
    void Start()
    {
        grabRadius = 1; 
    }

    // Update is called once per frame
    void Update()
    {
        if (!grabbing && Input.GetAxis(buttonName) == 1)
        {
            GrabObject();
        }

        if (grabbing && Input.GetAxis(buttonName) < 1)
        {
            DropObject();
        }
    }

    void GrabObject()
    {
        grabbing = true;

        RaycastHit[] hits;

        // only react for objects in the correct layers
        hits = Physics.SphereCastAll(transform.position, grabRadius, transform.forward, 0.0f, grabMask);
    
        if (hits.Length > 0)
        {
            int closestHit = 0;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider != null) Debug.Log(hits[i].collider.name + " has been hit. Its distance is: " + hits[i].distance);

                if ((hits[i]).distance < hits[closestHit].distance)
                {
                    closestHit = i;
                    Debug.Log(hits[i].collider.name + " has been selected. Its distance is: " + hits[i].distance);

                }
            }

            grabbedObject = hits[closestHit].transform.gameObject;
            grabbedObject.GetComponent<Rigidbody>().isKinematic = true; // gravity dont work on obj while it is held
            grabbedObject.transform.position = transform.position;
            grabbedObject.transform.parent = transform; // makes obj child of ctrler so they move tgt
        }
    }

    void DropObject()
    {
        grabbing = false;

        if (grabbedObject != null)
        {
            grabbedObject.transform.parent = null; // makes obj child of ctrler so they move tgt

            grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // gravity dont work on obj while it is held

            grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller); 
            grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller);

            grabbedObject = null; // makes obj child of ctrler so they move tgt
        }
    }

    public void AdjustRadius(float newRadius)
    {
        grabRadius = newRadius;
    }
}
