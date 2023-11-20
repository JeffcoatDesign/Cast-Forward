using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip[] _walkSounds;
    private float _walkSoundSpacing = 0.2f;
    public EnemyEntity enemyEntity;
    private Vector3 lastPosition;
    public EnemyMeleeHitbox weaponHitbox;
    private EnemyAI _enemyAI;
    public UnityEvent OnResurrected;
    float _timeSinceWalkSound = 0;
    private void Start()
    {
        OnResurrected ??= new ();
        _enemyAI = GetComponent<EnemyAI> ();
    }
    public void OnEnable()
    {
        enemyEntity.OnDeath += Die;
        enemyEntity.OnResurrect += Resurrect;
        enemyEntity.OnStagger += GetHit;
    }
    public void OnDisable()
    {
        enemyEntity.OnDeath -= Die;
        enemyEntity.OnResurrect -= Resurrect;
        enemyEntity.OnStagger -= GetHit;
    }

    private void Die ()
    {
        StartCoroutine(StartStaticAnim("Fall1"));
        weaponHitbox.isAttacking = false;
    }

    public void Attack ()
    {
        StartCoroutine(StartStaticAnim("Attack1h1"));
    }
    public void GetHit ()
    {
        StartCoroutine(StartStaticAnim("Hit1"));
        weaponHitbox.isAttacking = false;
    }

    void Resurrect ()
    {
        StartCoroutine(WaitForResurrected());
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
            _animator.SetFloat("speedh", speed);
        }
        lastPosition = transform.position;

        weaponHitbox.isAttacking = _animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1");
    }

    IEnumerator StartStaticAnim(string name)
    {
        _enemyAI.inStaticAnimation = true;
        _animator.SetTrigger(name);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _enemyAI.inStaticAnimation = false;

        yield return null;
    }

    IEnumerator WaitForResurrected ()
    {
        _animator.SetTrigger("Resurrect");
        _enemyAI.inStaticAnimation = true;
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        _animator.SetTrigger("Up");
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        OnResurrected?.Invoke();
        _enemyAI.inStaticAnimation = false;
        yield return null;
    }
}
