using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pigeon Event Channel", menuName = "Pigeon Event Channel")]
public class PigeonEventChannel : ScriptableObject
{
    public Action<GameObject> OnChange;
    public GameObject pigeon;

    public void Publish(GameObject obj)
    {
        pigeon = obj;
        OnChange.Invoke(pigeon);
    }
}
