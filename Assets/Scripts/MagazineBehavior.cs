using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineBehavior : MonoBehaviour
{
    [SerializeField] private EventChannel reloadGunEvent;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        {
            // Reloads the gun
            reloadGunEvent.Publish();
            // TODO: add reload gun animation (if hv time)
            Destroy(gameObject);
        }
    }
}
