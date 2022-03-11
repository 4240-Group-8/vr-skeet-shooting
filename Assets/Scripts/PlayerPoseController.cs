using UnityEngine;

public class PlayerPoseController : MonoBehaviour
{
    public PlayerTeleportedSO PlayerTeleported;
    private void Awake()
    {
        PlayerTeleported.OnChange += Teleport;
    }

    private void OnDestroy()
    {
        PlayerTeleported.OnChange -= Teleport;
    }

    private void Teleport(Vector3 position)
    {
        transform.position = position;
    }
}
