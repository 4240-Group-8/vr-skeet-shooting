using UnityEngine;

public class Teleport : MonoBehaviour
{
    public EventChannel playerTeleported;
    public string triggerButton;
    private bool _cooledDown = true;
    public float coolDownInSeconds;
    private float _currentCoolDown = 0.0f; // 0 is cooled down.
    void Start()
    {
        // subscribe methods that you want to happen when the event fires.
        // playerTeleported.OnChange += MethodName;
    }

    private void OnDestroy()
    {
        // unsubscribe methods that you did earlier
        // playerTeleported.OnChange -= MethodName;
    }

    void Update()
    {
        if (_cooledDown)
        {
            if (Input.GetAxis(triggerButton) == 1)
            {
                TeleportPlayer();
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

    private void TeleportPlayer()
    {
        // do the teleporting
        playerTeleported.Publish(); // currently plays the teleport sound
    }
}
