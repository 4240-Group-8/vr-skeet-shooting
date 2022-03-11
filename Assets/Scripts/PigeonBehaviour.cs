using UnityEngine;
/// <summary>
/// Lets clay pigeons play sounds and add points when the behaviour is appropriate.
/// This class assumes that it will be attached to the pigeon prefab.
/// </summary>
public class PigeonBehaviour : MonoBehaviour
{
    public EventChannel pickup;
    public EventChannel pointScored;
    public EventChannel pointNotScored;
    public GameObject smoke;
    public AudioSource pickUpSound;
    // the breaking sound is placed on the smoke
    // cuz it wouldn't play if placed on this pigeon and the pigeon gets destroyed.
    void Start()
    {
        pickup.OnChange += PickUp;
    }

    private void OnDestroy()
    {
        pickup.OnChange -= PickUp;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameObject breakingSmoke = Instantiate(this.smoke, gameObject.transform.position, gameObject.transform.rotation);
            breakingSmoke.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            pointScored.Publish();
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            GameObject breakingSmoke = Instantiate(this.smoke, gameObject.transform.position, gameObject.transform.rotation);
            breakingSmoke.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            pointNotScored.Publish();
        }
        else
        {
            // bounce off the object and do nothing lah
        }
    }
    public void PickUp()
    {
        pickUpSound.Play();
    }
}
