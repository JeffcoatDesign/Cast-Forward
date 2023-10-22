using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public EnemyEntity enemyEntity;
    private Vector3 lastPosition;
    public EnemyMeleeHitbox weaponHitbox;

    public void OnEnable()
    {
        enemyEntity.OnDeath += Die;
    }
    public void OnDisable()
    {
        enemyEntity.OnDeath -= Die;
    }

    private void Die ()
    {
        _animator.SetTrigger("Fall1");
    }

    public void Attack ()
    {
        _animator.SetTrigger("Attack1h1");
    }
    private void Update()
    {
        float speed = (transform.position - lastPosition).magnitude / Time.deltaTime;
        if (enemyEntity.isAlive)
        {
            _animator.SetFloat("speedv", speed);
        }
        lastPosition = transform.position;

        weaponHitbox.isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1");
    }
}
