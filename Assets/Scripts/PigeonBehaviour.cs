using UnityEngine;
using UnityEngine.Events;
public class PigeonBehaviour : MonoBehaviour
{
    public GameObject breakSmoke;
    private ScoreCounter _sc;
    private UnityEvent _addScore = new UnityEvent();
    void Start()
    {
        _sc = FindObjectOfType<Canvas>().GetComponent<ScoreCounter>();
        // created the event to make adding sound easier
        _addScore.AddListener(delegate 
        {
            _sc.AddPoint();
        });
    }
    void OnCollisionEnter(Collision collision)
    {
        // if (collision.gameObject.CompareTag("Bullet")) // to be added to bullet later on by @wenjin || @syasya
        if (false)
        {
            GameObject smoke = Instantiate(breakSmoke, gameObject.transform.position, gameObject.transform.rotation);
            smoke.GetComponent<AudioSource>().Play();
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
}
