using UnityEngine;

/// <summary>
/// Destroys bullet once it touches something.
/// </summary>
public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bulletImpactPrefab;
    [SerializeField] private float bulletHoleTimer = 2f;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gun") || collision.gameObject.CompareTag("Magazine"))
        { return; }
        
        // Collision with environment, instantiate the bullet impact
        if (!collision.gameObject.CompareTag("Pigeon"))
        {
            // Create the bullet impact
            GameObject newImpact = Instantiate(bulletImpactPrefab, gameObject.transform.position, gameObject.transform.rotation);
            newImpact.transform.forward = collision.gameObject.transform.forward;
            newImpact.transform.parent = collision.transform;
            
            // Destroy the bullet impact
            Destroy(newImpact, bulletHoleTimer);
        }
        
        Destroy(gameObject);
    }
}
