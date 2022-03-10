using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Allows player to equip gun and shoot bullets
/// TODO: enable gun after player throws pigeon off their hand
/// </summary>
public class Gun : MonoBehaviour
{
    [Header("Event Channel References")]
    public EventChannel gunShotAudioEvent;
    [SerializeField] private EventChannel reloadGunAudioEvent;

    [Header("Prefab References")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("References")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Controller axis that triggers the shot")] [SerializeField] private string triggerButton = "RHTrigger";
    [Tooltip("Specify time to destroy the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
    
    public bool canShoot;
    
    // Timer
    [Tooltip("Cooldown till the next fire")] [SerializeField] private float coolDownInSeconds = 10f;
    private bool _cooledDown = true;
    private float _currentCoolDown; // 0 is cooled down.
    
    // Ammo
    private int ammoCapacity = 10;
    private int _ammo;
    private bool HasAmmo { get; set; }


    [Header("Reload System References")]
    private bool _magazineInGun;
    [SerializeField] private Transform magazinePosition;
    [SerializeField] private GameObject magazineObject { get; set; }
    public bool ToReleaseMagazine { get; set; }
    
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
        _cooledDown = true;
        // HasAmmo = true;
        // _ammo = ammoCapacity;
    }

    void Update()
    {
        if (ToReleaseMagazine && _magazineInGun)
        {
            ReleaseMagazineFromGun();
        }

        if (_ammo <= 0)
        {
            HasAmmo = false;
        }


        if (_cooledDown && canShoot) // && _HasAmmo)
        {
            if (Input.GetAxis(triggerButton) == 1)
            {
                ShootGun();
            }
        }
        else
        {
            CoolDown();
        }
    }

    private void ReleaseMagazineFromGun()
    {
        ToReleaseMagazine = false;
        if (magazineObject != null) // we have a magazine in the gun
        {
            // TODO : Play release magazine sound
            magazineObject.transform.parent = null;
            
            // make the magazine fall out of the gun
            var rigidBody = magazineObject.GetComponent<Rigidbody>();
            rigidBody.isKinematic = false;
            rigidBody.AddForce(-magazineObject.transform.up * 3, ForceMode.Impulse);
            
            // reset magazine status
            _magazineInGun = false;
            magazineObject = null;
        }
    }


    private void ShootGun()
    {
        // Decrease the ammo count after shooting a bullet
        _ammo--;

        // Calls animation on the gun that has the relevant animation events that will fire
        gunAnimator.SetTrigger("Fire");

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation)
            .GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        
        // Show muzzle flash
        ShowMuzzleFlash();
        
        // Create a casing at the ejection slot
        CasingRelease();

        // Fire event to play shooting audio
        gunShotAudioEvent.Publish();

        _cooledDown = false;
        _currentCoolDown = coolDownInSeconds;
    }

    private void ShowMuzzleFlash()
    {
        if (muzzleFlashPrefab)
        {
            // Create the muzzle flash
            GameObject tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            // Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }
    }

    // This function creates a casing at the ejection slot
    void CasingRelease()
    {
        // Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        // Create the casing
        GameObject tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Magazine"))
        {
            if (!_magazineInGun)
            {
                // Play reload audio
                reloadGunAudioEvent.Publish();
                magazineObject = other.gameObject;
                var rigidBody = magazineObject.GetComponent<Rigidbody>();
                rigidBody.isKinematic = true;
                
                // disable collider
                var collider = magazineObject.GetComponent<Collider>();
                collider.transform.parent = magazinePosition;
                magazineObject.transform.position = magazinePosition.position;
                magazineObject.transform.rotation = magazinePosition.rotation;
                _magazineInGun = true;
                _ammo = ammoCapacity;
            }
        }
    }
}
