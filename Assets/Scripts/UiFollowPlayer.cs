using UnityEngine;

/// Attach this script to the Ui which is following the player
public class UiFollowPlayer : MonoBehaviour
{
    public Transform hoverPos; // A hovering follow target under the player 
                               // that this Ui will follow
    public float positionSpeedMultiplier = 1;
    public float rotateSpeedMultiplier = 1;
    private float positionSmoothTime = 0.2f;
    private float rotateSpeed = 0.05f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // initialOffset = player.transform.position - transform.position
        
    }
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
    }

}
