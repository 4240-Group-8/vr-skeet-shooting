using UnityEngine;

/// <summary>
/// Lets the player grab the gun using the right controller.
/// <author>ian-from-dover, syasyazman, nijnxw</author>
/// feel free to add your authorship here
/// </summary>
public class GrabRight : MonoBehaviour
{
    public EventChannel gunEquipped;
    public OVRInput.Controller Controller;
    public string buttonName = "RHGrab";
    public Gun shootingGun;
    public Transform holdingPos;

    private GameObject _grabbedObject;
    private bool _grabbing;
    
    void Update()
    {
        if (!_grabbing && Input.GetAxis(buttonName) == 1)
        {
            GrabGun();
        }

        if (_grabbing && Input.GetAxis(buttonName) < 1)
        {
            DropGun();
        }
    }

    void GrabGun()
    {
        _grabbing = true;

        // Just grab gun regardless of distance
        shootingGun.canShoot = true;
        _grabbedObject = shootingGun.gameObject;
        _grabbedObject.GetComponent<Rigidbody>().isKinematic = true; // gravity dont work on obj while it is held
        _grabbedObject.transform.position = holdingPos.position;
        _grabbedObject.transform.rotation = holdingPos.rotation;
        _grabbedObject.transform.parent = holdingPos; // makes obj child of ctrler so they move tgt
    }

    void DropGun()
    {
        _grabbing = false;

        if (_grabbedObject != null)
        {
            shootingGun.canShoot = false;

            _grabbedObject.transform.parent = null; // makes obj child of ctrler so they move tgt

            _grabbedObject.GetComponent<Rigidbody>().isKinematic = false; // gravity dont work on obj while it is held

            _grabbedObject.GetComponent<Rigidbody>().velocity = OVRInput.GetLocalControllerVelocity(Controller); 
            _grabbedObject.GetComponent<Rigidbody>().angularVelocity = OVRInput.GetLocalControllerAngularVelocity(Controller);

            _grabbedObject = null;
        }
    }
}
