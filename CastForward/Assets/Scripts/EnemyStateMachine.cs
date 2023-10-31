using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip[] _walkSounds;
    private float _walkSoundSpacing = 0.2f;
    public EnemyEntity enemyEntity;
    private Vector3 lastPosition;
    public EnemyMeleeHitbox weaponHitbox;
    float _timeSinceWalkSound = 0;
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
        _timeSinceWalkSound += Time.deltaTime;
        if (speed > 0 && _timeSinceWalkSound > _walkSoundSpacing)
        {
            int randomIndex = Random.Range(0,_walkSounds.Length);
            AudioManager.Instance.PlaySound(_walkSounds[randomIndex], AudioType.SFX, transform);
            _timeSinceWalkSound = 0;
        }
        if (enemyEntity.isAlive)
        {
            _animator.SetFloat("speedv", speed);
        }
        lastPosition = transform.position;

        weaponHitbox.isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1");
    }
}
