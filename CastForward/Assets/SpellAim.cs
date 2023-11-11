using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAim : MonoBehaviour
{
    [SerializeField] Transform _raycastTransform;
    [SerializeField] LayerMask _whatToHit;
    private void Update()
    {
        if (Physics.Raycast(_raycastTransform.position, _raycastTransform.forward, out RaycastHit hit, 100f, _whatToHit)) {
            transform.LookAt(hit.point);
        }
        else transform.eulerAngles = Vector3.zero;
    }
}
