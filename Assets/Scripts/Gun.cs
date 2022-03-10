using UnityEngine;

/// <summary>
/// Allows player to equip gun and shoot bullets
/// TODO: enable gun after player throws pigeon off their hand
/// </summary>
public class Gun : MonoBehaviour
{
    [Header("Event Channel References")]
    public EventChannel gunShot;

    [Header("Prefab References")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("References")]
    [SerializeField] private GameObject gunObject;
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Controller axis that triggers the shot")] [SerializeField] private string triggerButton = "RHTrigger";
    [Tooltip("Specify time to destroy the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;

    private bool _cooledDown = true;
    public float coolDownInSeconds;
    private float _currentCoolDown = 0.0f; // 0 is cooled down.

    public bool canShoot = false;

    void Start()
    {
        _cooledDown = true;
    }

    private void OnDestroy()
    {

    }

    void Update()
    {
        if (_cooledDown && canShoot)
        {
            if (Input.GetAxis(triggerButton) == 1)
            {
                ShootGun();
                MarkGunAsJustFired();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void MarkGunAsJustFired()
    {
        _cooledDown = false;
        _currentCoolDown = coolDownInSeconds;
    }

    private void ShootGun()
    {
        if (muzzleFlashPrefab)
        {
            // Create the muzzle flash
            GameObject tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        // Cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }

        // Calls animation on the gun that has the relevant animation events that will fire
        if (gunAnimator != null)
        { gunAnimator.SetTrigger("Fire"); }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        // Create a casing at the ejection slot
        CasingRelease();

        // Fire event to play shooting audio
        gunShot.Publish();
    }

    // This function creates a casing at the ejection slot
    void CasingRelease()
    {
        // Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        // Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        // Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        // Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        // Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

    // Updates gun cooldown
    private void CoolDown()
    {
        if (_currentCoolDown > 0)
        {
            _currentCoolDown -= Time.deltaTime;
        }
        else
        {
            _cooledDown = true;
            _currentCoolDown = 0;
        }
    }
}
