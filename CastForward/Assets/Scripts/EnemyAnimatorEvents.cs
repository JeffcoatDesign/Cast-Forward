using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimatorEvents : MonoBehaviour
{
    public UnityEvent OnAnimCastSpell;
    private void Start()
    {
        OnAnimCastSpell ??= new();
    }
    public void AnimCast ()
    {
        OnAnimCastSpell.Invoke();
    }
}
