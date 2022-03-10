using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Plays all the audio in the game.
/// In particular, manages the crowd's cheering behaviour.
/// </summary>
public class AudioEventManager : MonoBehaviour
{
    // Event Channels in the order that they happen
    public EventChannel playerTeleported;
    public EventChannel timeSlowed;
    public EventChannel timeResumed;
    public EventChannel gunEquipped;
    public EventChannel gunUnequipped;
    public EventChannel gunShot;
    public EventChannel pointScored;
    public EventChannel pointNotScored;
    public EventChannel pigeonPickedUp;
    public EventChannel timerStarted;
    public EventChannel timerStopped;
    public EventChannel timerReset;
    
    // s stands for (sound)
    public GameObject playerCamera;
    private AudioSource _sTeleport; 
    private AudioSource _sTimeSlow; 
    private AudioSource _sTimeResume; 
    
    public GameObject gunHand;
    private AudioSource _sEquipGun; 
    private AudioSource _sShootGun; 
    
    public GameObject timerCanvas;
    private AudioSource _sTimerStart; 
    private AudioSource _sTimerStop; 
    private AudioSource _sTimerReset;
    private bool _timerIsCounting = true; // for the initial crowd sound
    
    // N S E W north south east west
    public GameObject nCrowd;
    private AudioSource _sCrowdCheerN; 
    private AudioSource _sCrowdSadN; 
    private AudioSource _sCrowdNormalLoopN;
    private AudioSource _sCrowdExcitedLoopN;
    
    public GameObject sCrowd;
    private AudioSource _sCrowdCheerS; 
    private AudioSource _sCrowdSadS; 
    private AudioSource _sCrowdNormalLoopS;
    private AudioSource _sCrowdExcitedLoopS;
    
    public GameObject eCrowd;
    private AudioSource _sCrowdCheerE; 
    private AudioSource _sCrowdSadE; 
    private AudioSource _sCrowdNormalLoopE;
    private AudioSource _sCrowdExcitedLoopE;
    
    public GameObject wCrowd;
    private AudioSource _sCrowdCheerW; 
    private AudioSource _sCrowdSadW; 
    private AudioSource _sCrowdNormalLoopW;
    private AudioSource _sCrowdExcitedLoopW;

    private List<AudioSource> _cheers = new List<AudioSource>();
    private List<AudioSource> _sads = new List<AudioSource>();
    private List<AudioSource> _normalLoops = new List<AudioSource>();
    private List<AudioSource> _excitedLoops = new List<AudioSource>();
    void Start()
    {
        // Impossible to specify in editor which sound component on the same gameObject to assign
        AudioSource[] playerSounds = playerCamera.GetComponents<AudioSource>();
        _sTeleport = playerSounds[0];
        _sTimeSlow = playerSounds[1];
        _sTimeResume = playerSounds[2];
        
        AudioSource[] gunSounds = gunHand.GetComponents<AudioSource>();
        _sShootGun = gunSounds[0];
        _sEquipGun = gunSounds[1];
        
        AudioSource[] timerSounds = timerCanvas.GetComponents<AudioSource>();
        _sTimerStart = timerSounds[0];
        _sTimerStop = timerSounds[1];
        _sTimerReset = timerSounds[2];
        
        AudioSource[] nCrowdSounds = nCrowd.GetComponents<AudioSource>();
        _sCrowdCheerN = nCrowdSounds[0];
        _sCrowdSadN = nCrowdSounds[1];
        _sCrowdNormalLoopN = nCrowdSounds[2];
        _sCrowdExcitedLoopN = nCrowdSounds[3];
        _cheers.Add(_sCrowdCheerN);
        _sads.Add(_sCrowdSadN);
        _normalLoops.Add(_sCrowdNormalLoopN);
        _excitedLoops.Add(_sCrowdExcitedLoopN);
        
        AudioSource[] sCrowdSounds = sCrowd.GetComponents<AudioSource>();
        _sCrowdCheerS = sCrowdSounds[0];
        _sCrowdSadS = sCrowdSounds[1];
        _sCrowdNormalLoopS = sCrowdSounds[2];
        _sCrowdExcitedLoopS = sCrowdSounds[3];
        _cheers.Add(_sCrowdCheerS);
        _sads.Add(_sCrowdSadS);
        _normalLoops.Add(_sCrowdNormalLoopS);
        _excitedLoops.Add(_sCrowdExcitedLoopS);
        
        AudioSource[] eCrowdSounds = eCrowd.GetComponents<AudioSource>();
        _sCrowdCheerE = eCrowdSounds[0];
        _sCrowdSadE = eCrowdSounds[1];
        _sCrowdNormalLoopE = eCrowdSounds[2];
        _sCrowdExcitedLoopE = eCrowdSounds[3];
        _cheers.Add(_sCrowdCheerE);
        _sads.Add(_sCrowdSadE);
        _normalLoops.Add(_sCrowdNormalLoopE);
        _excitedLoops.Add(_sCrowdExcitedLoopE);
        
        AudioSource[] wCrowdSounds = wCrowd.GetComponents<AudioSource>();
        _sCrowdCheerW = wCrowdSounds[0];
        _sCrowdSadW = wCrowdSounds[1];
        _sCrowdNormalLoopW = wCrowdSounds[2];
        _sCrowdExcitedLoopW = wCrowdSounds[3];
        _cheers.Add(_sCrowdCheerW);
        _sads.Add(_sCrowdSadW);
        _normalLoops.Add(_sCrowdNormalLoopW);
        _excitedLoops.Add(_sCrowdExcitedLoopW);
        
        // Subscribe methods to event channels
        pigeonPickedUp.OnChange += CrowdExcitedLoop;
        pointScored.OnChange += CrowdNormalLoop;
        pointScored.OnChange += CrowdCheer;
        pointScored.OnChange += PigeonDestroyed;
        pointNotScored.OnChange += PigeonDestroyed;
        pointNotScored.OnChange += CrowdNormalLoop;
        pointNotScored.OnChange += CrowdSad;
        gunEquipped.OnChange += EquipGun;
        gunShot.OnChange += ShootGun;
        playerTeleported.OnChange += Teleport;
        timeSlowed.OnChange += SlowTime;
        timeResumed.OnChange += ResumeTime;
        timerStarted.OnChange += StartTimer;
        timerStopped.OnChange += CrowdNormalLoop;
        timerStopped.OnChange += StopTimer;
        timerReset.OnChange += ResetTimer;
        
        CrowdNormalLoop();
        _timerIsCounting = false;
    }
    
    void OnDestroy()
    {
        // unsubscribe event channels to prevent resource leak
        pigeonPickedUp.OnChange -= CrowdExcitedLoop;
        pointScored.OnChange -= CrowdNormalLoop;
        pointScored.OnChange -= CrowdCheer;
        pointScored.OnChange -= PigeonDestroyed;
        pointNotScored.OnChange -= PigeonDestroyed;
        pointNotScored.OnChange -= CrowdNormalLoop;
        pointNotScored.OnChange -= CrowdSad;
        gunEquipped.OnChange -= EquipGun;
        gunShot.OnChange -= ShootGun;
        playerTeleported.OnChange -= Teleport;
        timeSlowed.OnChange -= SlowTime;
        timeResumed.OnChange -= ResumeTime;
        timerStarted.OnChange -= StartTimer;
        timerStopped.OnChange -= CrowdNormalLoop;
        timerStopped.OnChange -= StopTimer;
        timerReset.OnChange -= ResetTimer;
    }

    void PigeonDestroyed()
    {
        gunUnequipped.Publish();
    }

    private void Teleport()
    {
        _sTeleport.Play();
    }
    private void SlowTime()
    {
        _sTimeSlow.Play();
    }
    private void ResumeTime()
    {
        _sTimeResume.Play();
    }
    private void StartTimer()
    {
        _sTimerStart.Play();
        _timerIsCounting = true;
    }
    private void StopTimer()
    {
        _sTimerStop.Play();
        _timerIsCounting = false;
    }
    private void ResetTimer()
    {
        _sTimerReset.Play();
        _timerIsCounting = false;
    }
    private void CrowdCheer()
    {
        if (_timerIsCounting)
        {
            PlayCrowdSound(_cheers);
        }
    }
    private void CrowdSad()
    {
        if (_timerIsCounting)
        {
            PlayCrowdSound(_sads);
        }
    }
    private void CrowdNormalLoop()
    {
        if (_timerIsCounting)
        {
            PlayCrowdSound(_normalLoops);
            StopCrowdSound(_excitedLoops);
        }
    }
    private void CrowdExcitedLoop()
    {
        if (_timerIsCounting)
        {
            PlayCrowdSound(_excitedLoops);
            StopCrowdSound(_normalLoops);
        }
    }
    private void PlayCrowdSound(List<AudioSource> sound)
    {
        foreach (AudioSource crowdSound in sound)
        {
            crowdSound.Play();
        }
    }

    private void StopCrowdSound(List<AudioSource> sound) 
    {
        foreach (AudioSource crowdSound in sound)
        {
            crowdSound.Stop();
        }
    }

    private void EquipGun()
    {
        _sEquipGun.Play();
    }
    private void ShootGun()
    {
        _sShootGun.Play();
    }
}
