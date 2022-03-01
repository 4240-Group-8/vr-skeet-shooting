using UnityEngine;

/// <summary>
/// Spawns a pigeon in left hand
/// Attached to LH
/// </summary>
public class SpawnPigeon : MonoBehaviour
{
    public GameObject LH;
    public GameObject pigeon;
    public EventChannel pigeonSpawn;
    void Update()
    {
        if (Input.GetButtonDown("Spawn")) // X, joystick button 2, on L ctrller
        {
            GameObject spawned = Instantiate(pigeon, LH.transform.position, LH.transform.rotation);
            // how do i pass this reference to the event?
            // Todo: create a new pigeoneventchannel that takes in pigeon
            pigeonSpawn.Publish();
            // pigeonSpawn.Publish(spawned);
        }
    }
}
