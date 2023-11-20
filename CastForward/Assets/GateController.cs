using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private Animator _animator;
    public bool IsOpen = true;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        TriggerGate(IsOpen);
    }

    public void TriggerGate (bool isOpen)
    {
        IsOpen = isOpen;
        _animator.SetBool("isOpen", isOpen);
    }
}
