using UnityEngine;

/// <summary>
/// Spawns a pigeon in left hand
/// Attached to LH
/// </summary>
public class SpawnPigeon : MonoBehaviour
{
    public GameObject LH;
    public GameObject pigeon;
    void Update()
    {
        if (Input.GetButtonDown("Spawn")) // X, joystick button 2, on L ctrller
        {
            Instantiate(pigeon, LH.transform.position, LH.transform.rotation);
        }
    }
}
