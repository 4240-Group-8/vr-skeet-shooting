using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Spawns a pigeon in left hand
/// </summary>
public class SpawnPigeon : MonoBehaviour
{
    public GameObject LH;
    public GameObject pigeon;
    void Update()
    {
        if (Input.GetButtonDown("Spawn")) // X, joystick button 2, on L ctrller
        {
            GameObject spawned = Instantiate(pigeon, LH.transform.position, LH.transform.rotation);
        }
    }
}
