using UnityEngine;
/// <summary>
/// Lets clay pigeons play sounds and add points when the behaviour is appropriate.
/// This class assumes that it will be attached to the pigeon prefab.
/// </summary>
public class PigeonBehaviour : MonoBehaviour
{
    public EventChannel pickup;
    public EventChannel pointScored;
    public EventChannel pointNotScored; // assign below when shooting is implemented
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
        if (collision.gameObject.CompareTag("Bullet")) // to be added to bullet later on by @wenjin || @syasya (done)
        {
            GameObject breakingSmoke = Instantiate(this.smoke, gameObject.transform.position, gameObject.transform.rotation);
            breakingSmoke.GetComponent<AudioSource>().Play();
            pointScored.Publish();
            Debug.Log("Hit bullet");
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            GameObject breakingSmoke = Instantiate(this.smoke, gameObject.transform.position, gameObject.transform.rotation);
            breakingSmoke.GetComponent<AudioSource>().Play();
            pointScored.Publish(); // change to PointNotScored when bullets are implemented
            Destroy(gameObject);
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
