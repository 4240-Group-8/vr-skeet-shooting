using UnityEngine;

/// <summary>
/// Allows player to equip gun and shoot bullets
/// TODO: enable gun after player throws pigeon off their hand
/// </summary>
public class Shoot : MonoBehaviour
{
    [Header("Event Channel References")]
    public EventChannel gunEquipped;
    public EventChannel gunUnequipped;
    public EventChannel gunShot;
    public string triggerButton = "RHTrigger";

    [Header("Prefab References")]
    public GameObject gunPrefab;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    
    [Header("Location References")]
    [SerializeField] private Transform barrelLocation;
    
    [Header("Settings")]
    [Tooltip("Specify time to destroy the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;
    
    private bool _gunEnabled = false;
    private bool _cooledDown = true;
    public float coolDownInSeconds;
    private float _currentCoolDown = 0.0f; // 0 is cooled down.
    void Start()
    {
        // subscribes the methods to these events. it will activate when the event happens
        gunEquipped.OnChange += EquipGun;
        gunUnequipped.OnChange += UnequipGun;
    }

    private void OnDestroy()
    {
        gunEquipped.OnChange -= EquipGun;
        gunUnequipped.OnChange -= UnequipGun;
    }

    void Update()
    {
        if (_cooledDown)
        {
            if (_gunEnabled && Input.GetAxis(triggerButton) == 1)
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
            GameObject tempFlash  = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //Destroy the muzzle flash effect
            Destroy(tempFlash, destroyTimer);
        }

        // Cancels if there's no bullet prefeb
        if (!bulletPrefab)
        { return; }
        
        // Create a bullet and add force on it in direction of the barrel
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation)
            .GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        gunShot.Publish();
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

    private void EquipGun()
    {
        _gunEnabled = true;
        gunPrefab.SetActive(true);
    }
    
    private void UnequipGun()
    {
        _gunEnabled = false;
        gunPrefab.SetActive(false);
    }
}
