using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    EnemyStateMachine _enemyStateMachine;
    void Awake()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        if ( _enemyStateMachine != null )
        {
            _enemyStateMachine.IsMelee = true;
        }
    }

    public void Attack ()
    {
        if (_enemyStateMachine != null) {
            _enemyStateMachine.StartTrigger("Attack1h1");
        }
    }
}
