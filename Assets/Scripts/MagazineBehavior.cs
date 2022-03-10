using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineBehavior : MonoBehaviour
{
    [SerializeField] private EventChannel reloadGunEvent;
    [SerializeField] private EventChannel reloadGunAudioEvent;
    [SerializeField] private Transform reloadMagCollider;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform == reloadMagCollider)
        {
            // Reloads the gun
            reloadGunEvent.Publish();
            reloadGunAudioEvent.Publish();
            Destroy(gameObject);
        }
    }
}
