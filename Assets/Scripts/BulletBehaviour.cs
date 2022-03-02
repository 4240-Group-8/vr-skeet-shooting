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
        Destroy(gameObject);
    }
}
