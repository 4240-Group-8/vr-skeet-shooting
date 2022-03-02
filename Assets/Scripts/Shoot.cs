using UnityEngine;

/// <summary>
/// Allows player to equip gun and shoot bullets
/// TODO: enable gun after player throws pigeon off their hand
/// </summary>
public class Shoot : MonoBehaviour
{
    public EventChannel gunEquipped;
    public EventChannel gunUnequipped;
    public EventChannel gunShot;
    public string triggerButton;
    public GameObject gunNozzle;
    public GameObject bullet;
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
        Instantiate(bullet, gunNozzle.transform.position, gunNozzle.transform.rotation); // give it some extreme speed with the forward vector i'll leave you to do it
        gunShot.Publish();
    }

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
    }
    private void UnequipGun()
    {
        _gunEnabled = false;
    }
}
