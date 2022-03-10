using UnityEngine;
/// <summary>
/// To be placed on the collider child of the hoop prefab.
/// Makes hoops slow time for the player and change to green.
/// When the pigeon touches the ground, the hoop is destroyed also.
///
/// TODO: Bad OOP, but the only way I can think of now is to put all this behaviour on the pigeon
/// </summary>
public class HoopBehaviour : MonoBehaviour
{
    public EventChannel timeSlowed;
    public EventChannel gunUnequipped;
    public Material green;
    public static GameObject parentHoop;
    public GameObject smoke;
    void OnEnable()
    {
        timeSlowed.OnChange += ChangeColour;
        gunUnequipped.OnChange += ClearHoop;
    }

    private void OnDisable()
    {
        timeSlowed.OnChange -= ChangeColour;
        gunUnequipped.OnChange -= ClearHoop;
    }
    
    /// <summary>
    /// When pigeon enters ring, time slows, ring changes colour
    /// </summary>
    /// <param name="collision">Details about the collision which happened.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pigeon"))
        {
            parentHoop = transform.parent.gameObject;
            timeSlowed.Publish();
        }
    }
    public void ChangeColour()
    {
        parentHoop.GetComponent<MeshRenderer>().material = green;
    }
    /// <summary>
    /// When pigeon hits ground and is destroyed,
    /// Destroy this hoop and emit smoke also.
    /// </summary>
    public void ClearHoop()
    {
        // problem: every hoop will make 1 smoke object at parenthoop there
        Instantiate(smoke, parentHoop.transform.position, parentHoop.transform.rotation);
        parentHoop.SetActive(false);
    }
}
