using System.Collections.Generic;
using UnityEngine;

public class GlobalHoopController : MonoBehaviour
{
    public EventChannel timerUiStarted;
    public EventChannel timerUiStopped;
    public EventChannel timerUiReset;
    public Material defaultMat;
    private List<GameObject> hoops;
    void Start()
    {
        hoops = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hoop"));
        timerUiStarted.OnChange += EnableAllHoops;
        timerUiStopped.OnChange += DisableAllHoops;
        timerUiReset.OnChange += DisableAllHoops;
        DisableAllHoops();
    }

    private void OnDestroy()
    {
        timerUiStarted.OnChange -= EnableAllHoops;
        timerUiStopped.OnChange -= DisableAllHoops;
        timerUiReset.OnChange -= DisableAllHoops;
    }

    void EnableAllHoops()
    {
        foreach (GameObject h in hoops)
        {
            h.SetActive(true);
            h.GetComponent<MeshRenderer>().material = defaultMat;
        }
    }
    
    void DisableAllHoops()
    {
        hoops.ForEach(h => h.SetActive(false));
    }

}
