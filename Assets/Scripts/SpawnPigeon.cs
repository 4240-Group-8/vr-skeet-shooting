using UnityEngine;

/// <summary>
/// Spawns a pigeon in left hand
/// Attached to LH
/// </summary>
public class SpawnPigeon : MonoBehaviour
{
    public GameObject LH;
    public GameObject pigeon;
    public OVRInput.Controller Controller; // placed on L and R
    public bool instantiated;

    void Update()
    {
        if (!instantiated && Input.GetAxis("Spawn") == 1) // X, joystick button 2, on L ctrller
        {
            Instantiate(pigeon, LH.transform.position, LH.transform.rotation);

            instantiated = true; // to allow only one spawn at a time
            // let pigeon continue being grabbed by hand
            pigeon.GetComponent<Rigidbody>().isKinematic = true; // gravity dont work on obj while it is held
            pigeon.transform.position = LH.transform.position;
            pigeon.transform.parent = LH.transform; // makes obj child of ctrler so they move tgt
        }

        if (instantiated && Input.GetAxis("Spawn") < 1) // let go of holding the button
        {
            pigeon.transform.parent = null;
            pigeon.GetComponent<Rigidbody>().isKinematic = false;
            pigeon.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller);
            pigeon.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller);

            instantiated = false;
        }
    }
}
