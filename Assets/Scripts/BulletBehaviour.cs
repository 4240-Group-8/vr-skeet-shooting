using UnityEngine;

/// <summary>
/// Destroys bullet once it touches something.
/// </summary>
public class BulletBehaviour : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun"))
        { return; }
        // PigeonBehaviour.cs handles smoke instantiation upon collision
        // Collision with environment, do nothing
        Destroy(gameObject);
    }
}
