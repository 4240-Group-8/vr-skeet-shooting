using UnityEngine;
/// <summary>
/// To be placed on the collider child of the hoop prefab.
/// Makes hoops slow time for the player and change to green.
/// When the pigeon touches the ground, the hoop is destroyed also.
/// </summary>
public class HoopBehaviour : MonoBehaviour
{
    // only start counting score when timer is running
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
        // only solution now: dont have smoke particles when parent has been destroyed
        
        // todo: event order got problem, fix after dinner for score counting
        // diagnosis: gunUnequip is turning countscore to false before addpoint can add the point.
        // solution: this problem wont exist once (only bullets) add score.
        if (parentHoop != null)
        {
            Instantiate(smoke, parentHoop.transform.position, parentHoop.transform.rotation);
            parentHoop.SetActive(false);
            parentHoop = null;
        }
    }

}
