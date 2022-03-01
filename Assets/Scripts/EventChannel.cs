using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event Channel", menuName = "Event Channel")]
/// A blueprint for an 'event' which is a ScriptableObject
/// Makes it much easier to subscribe many methods across classes to the event and call them.
/// Reduces coupling
public class EventChannel : ScriptableObject
{
    public Action OnChange;

    public void Publish()
    {
        OnChange.Invoke();
    }
}
