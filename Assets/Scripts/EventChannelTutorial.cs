using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChannelTutorial : MonoBehaviour
{
    // declare the event channel you wanna use and drag the scriptable object into the field in the unity editor
    public EventChannel playerTeleported;

    void Start()
    {
        // subscribe and unsubscribe methods to the eventchannel if you need the event to
        // call your methods when the event happens
        // if you're only calling .publish(); then you dont have to subscribe.
        playerTeleported.OnChange += PlaySound;
        // eventName.OnChange += yourMethod;
    }

    void OnDestroy()
    {
        // unsubscribe event channels to prevent resource leak
        playerTeleported.OnChange += PlaySound;
    }

    // this method will be called when the event happens
    void PlaySound()
    {
        Debug.Log("The event has happened!");
    }
    
    void MakeEventHappen()
    {
        // you can make the event happen in any method by simply calling .Publish() of the EventChannel
        // usually other classes call this
        playerTeleported.Publish();
    }
}
