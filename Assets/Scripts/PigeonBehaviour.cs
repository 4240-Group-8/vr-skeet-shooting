using UnityEngine;
using UnityEngine.Events;

public class PigeonBehaviour : MonoBehaviour
{
    public GameObject breakSmoke;
    public UnityEvent targetDestroyed;
    void Start()
    {
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // obsolete system
            Debug.Log("collided with bullet");
            // breakSound.Play();
            targetDestroyed.Invoke(); // add score
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("collided with else");
            GameObject smoke = Instantiate(breakSmoke, gameObject.transform.position, gameObject.transform.rotation);
            smoke.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
        }

    }
}
