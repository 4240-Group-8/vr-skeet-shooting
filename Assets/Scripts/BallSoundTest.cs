using UnityEngine;
using UnityEngine.Events;

public class BallSoundTest : MonoBehaviour
{
    public AudioSource breakSound;
    public UnityEvent targetDestroyed;
    void Start()
    {
        breakSound = GetComponent<AudioSource>();
    }
    void OnCollisionEnter(Collision collision)
    {
        // for ball
        if (collision.gameObject.tag == "Target")
        {
            breakSound.Play();
            targetDestroyed.Invoke();
            Destroy(collision.gameObject);
        }
    }
}
