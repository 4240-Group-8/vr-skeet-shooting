using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportable : MonoBehaviour
{
    public GameObject Indicator;

    public string InputIdentifier;

    private int _layerMask;

    private RaycastHit _raycastHit;
    // Start is called before the first frame update
    void Start()
    {
        _layerMask = 1 << 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(InputIdentifier) != 1)
        {
            return;
        }

        if (Physics.Raycast((transform.position), transform.forward, out _raycastHit, Mathf.Infinity, _layerMask))
        {
            Indicator.transform.position = _raycastHit.point;
            Debug.DrawRay(transform.position, transform.forward * _raycastHit.distance, Color.blue);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.forward * 1000, Color.white);
        }
    }
}
