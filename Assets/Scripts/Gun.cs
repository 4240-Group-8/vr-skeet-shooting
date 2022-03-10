using System;
using UnityEditor;
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
    [SerializeField] private EventChannel reloadGunEvent;

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
    [Tooltip("Cooldown till the next fire")] [SerializeField] private float coolDownInSeconds = 10f;
    
    private bool _cooledDown = true;
    private float _currentCoolDown; // 0 is cooled down.
    private int ammoCapacity = 10;
    private int _ammo;
    private bool _hasAmmo;

    public bool canShoot;

    void Start()
    {
        // reloadGunEvent.OnChange += Reload;
        _cooledDown = true;
        _hasAmmo = true;
        // _ammo = ammoCapacity;
    }

    private void OnDestroy()
    {
        // reloadGunEvent.OnChange -= Reload;
    }

    void Update()
    {
        if (_ammo <= 0)
        {
            // _hasAmmo = false;
        }

        if (_cooledDown && canShoot) // && _hasAmmo)
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

        // Cancels if there's no bullet prefab
        if (!bulletPrefab)
        { return; }

        // Calls animation on the gun that has the relevant animation events that will fire
        if (gunAnimator != null)
        { gunAnimator.SetTrigger("Fire"); }

        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation)
            .GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

        // Decrease the ammo count after shooting a bullet
        // _ammo--;
        
        // Create a casing at the ejection slot
        CasingRelease();

        // Fire event to play shooting audio
        gunShotAudioEvent.Publish();
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

    /*
    private void Reload()
    {
        _ammo = ammoCapacity;
        _hasAmmo = true;
    }
    */
}
