using UnityEngine;

/// Attach this script to the Ui which is following the player
public class UiFollowPlayer : MonoBehaviour
{
    public Transform hoverPos; // A hovering follow target under the player 
    // public Transform player; // if you want the menu to always look at the player but got to fix the mirroring of the Ui (idk why it happens)
    public float positionSpeedMultiplier = 1;
    public float rotateSpeedMultiplier = 1;
    private float positionSmoothTime = 0.2f;
    private float rotateSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Transform t = transform;
        t.position = Vector3.SmoothDamp(t.position, 
                                              hoverPos.position, 
                                              ref velocity,
                                              positionSmoothTime / positionSpeedMultiplier);
        t.rotation = Quaternion.Slerp(t.rotation,
                                        hoverPos.rotation, 
                                          rotateSpeed * rotateSpeedMultiplier);
        /*
        // Always look at player (assign player to main camera)
        
        // but for some reason it horizontally mirrors the canvas
        Vector3 relativePos = player.position - t.position;
        t.rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
        */
    }
    

}
