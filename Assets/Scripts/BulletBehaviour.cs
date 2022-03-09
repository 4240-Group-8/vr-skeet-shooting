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
        // PigeonBehaviour.cs handles smoke instantiation upon collision
        // Collision with environment, do nothing
        // TODO: implement decrease in bullets that player holds
        Destroy(gameObject);
    }
}
