using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventManager : MonoBehaviour
{
    // Event Channels
    public EventChannel playerTeleported;
    public EventChannel timeSlowed;
    public EventChannel timeResumed;
    public EventChannel gunEquipped;
    public EventChannel gunShot;
    public EventChannel pigeonPickedUp;
    public EventChannel pigeonSpawned;
    public EventChannel pigeonThrown;
    public EventChannel pointScored;
    public EventChannel pointNotScored;
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
    
    public GameObject pigeon; // bound when pigeonSpawn is called (when pigeon is spawned)
    private AudioSource _sPigeonPickup; 
    private AudioSource _sPigeonThrow; 
    private AudioSource _sPigeonBreak; 
    
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
        // Subscribe methods to event channels
        pigeonSpawned.OnChange += PigeonSpawned;
        
        
        // Impossible to specify in editor which sound on same gameObject to assign
        AudioSource[] playerSounds = playerCamera.GetComponents<AudioSource>();
        _sTeleport = playerSounds[0];
        _sTimeSlow = playerSounds[1];
        _sTimeResume = playerSounds[2];
        
        AudioSource[] gunSounds = gunHand.GetComponents<AudioSource>();
        _sEquipGun = gunSounds[0];
        _sShootGun = gunSounds[1];
        
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
    }

    void OnDestroy()
    {
        // unsubscribe event channels to prevent resource leak
        pigeonSpawned.OnChange -= PigeonSpawned;
    }

    void PigeonSpawned() // change this to have pigeon param
    {
        pigeon = GameObject.Find("ClayPigeon"); // but what if player spawns more than 1
        AudioSource[] pigeonSounds = pigeon.GetComponents<AudioSource>();
        _sPigeonPickup = pigeonSounds[0];
        _sPigeonThrow = pigeonSounds[1];
        _sPigeonBreak = pigeonSounds[2];
        
    }
}
