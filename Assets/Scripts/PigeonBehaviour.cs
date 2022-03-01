using UnityEngine;
using UnityEngine.Events;
/// <summary>
/// Lets clay pigeons play sounds and add points when the behaviour is appropriate.
/// This class assumes that it will be attached to the pigeon prefab.
/// </summary>
public class PigeonBehaviour : MonoBehaviour
{
    public GameObject breakSmoke;
    public AudioSource _pickUpSound; 
    public AudioSource _breakSound; 
    public AudioSource _throwSound;
    public EventChannel pickup;
    public EventChannel pigeonThrow;
    public EventChannel pointScored;
    public EventChannel pointNotScored;
    private ScoreCounter _sc;
    private UnityEvent _addScore = new UnityEvent();
    void Start()
    {
        _sc = FindObjectOfType<Canvas>().GetComponent<ScoreCounter>();
        // created the event to make adding sound easier
        _addScore.AddListener(delegate 
        {
        });
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet")) // to be added to bullet later on by @wenjin || @syasya
        {
            GameObject smoke = Instantiate(breakSmoke, gameObject.transform.position, gameObject.transform.rotation);
            // invoke score point
            // break sound, crowd cheer sound, add point
            _breakSound.Play();
            _addScore.Invoke();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Floor"))
        {
            GameObject smoke = Instantiate(breakSmoke, gameObject.transform.position, gameObject.transform.rotation);
            smoke.GetComponent<AudioSource>().Play();
            _addScore.Invoke(); // for debug
            Destroy(gameObject);
        }
        else
        {
            // bounce off and do nothing lah
        }
    }
    public void AddPoint()
    {
        _sc.AddPoint();
    }
    public void PickUp()
    {
        _pickUpSound.Play();
    }
    public void Throw()
    {
        _throwSound.Play(); // whoosh
    }
}
