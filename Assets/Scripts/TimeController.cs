using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public EventChannel TimeSlowed;
    public EventChannel TimeResumed;
    private float _fixedDeltaTime;

    private void Awake()
    {
        _fixedDeltaTime = Time.fixedDeltaTime;
        TimeSlowed.OnChange += SlowTime;
    }

    private void OnDestroy()
    {
        TimeSlowed.OnChange -= SlowTime;
    }

    void SlowTime()
    {
        Time.timeScale = 0.1f;
        Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
        StartCoroutine(ResumeTime());
    }

    IEnumerator ResumeTime()
    {
        yield return new WaitForSecondsRealtime(5);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = _fixedDeltaTime * Time.timeScale;
        TimeResumed.Publish();
    }
}
