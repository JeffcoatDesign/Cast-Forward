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
        StartCoroutine(QuicklyMove());
    }

    public void TriggerGate (bool isOpen)
    {
        IsOpen = isOpen;
        _animator.SetBool("isOpen", isOpen);
    }

    IEnumerator QuicklyMove ()
    {
        _animator.speed = 100f;
        TriggerGate(IsOpen);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _animator.speed = 1f;
    }
}
