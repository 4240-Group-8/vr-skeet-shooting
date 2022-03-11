using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class PlayerTeleportedSO : ScriptableObject
{
    public UnityAction<Vector3> OnChange;

    public void Publish(Vector3 position)
    {
        OnChange.Invoke(position);
    }
}