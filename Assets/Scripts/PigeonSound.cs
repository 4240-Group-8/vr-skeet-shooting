using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class PigeonSound : MonoBehaviour
{
    public AudioSource breakSound;
    public UnityEvent targetDestroyed;
    void Start()
    {
        breakSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Target")
        {
            breakSound.Play();
            targetDestroyed.Invoke();
            Destroy(collision.gameObject);
        }

    }
}
