using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioClip[] _walkSounds;
    private bool _isMelee;
    public bool IsMelee { get { return IsMelee; } set { _isMelee = value; } }
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
        StartCoroutine(StartStaticAnim(_animator.GetCurrentAnimatorClipInfo(0)[0].clip.length));
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
        if (_isMelee) weaponHitbox.isAttacking = false;
    }

    public void StartTrigger (string trigger, bool isStatic = true, float duration = -1f,string animFloat = "")
    {
        if (!enemyEntity.isAlive) return;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Fall1")) return;
        if (isStatic && duration < 0)
            StartCoroutine(StartStaticAnim(trigger));
        else
            _animator.SetTrigger(trigger);
        float animLength = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        if (duration > 0f && animFloat != "")
        {
            float offset = duration / animLength;
            _animator.SetFloat(animFloat, offset);
            if (isStatic)
                StartCoroutine(StartStaticAnim(animLength * offset));
        }
    }
    public void GetHit ()
    {
        StartCoroutine(StartStaticAnim("Hit1"));
        if (_isMelee)
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
            _animator.SetBool("Dead", !enemyEntity.isAlive);
        }
        lastPosition = transform.position;

        if (_isMelee)
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
    IEnumerator StartStaticAnim(float length)
    {
        _enemyAI.inStaticAnimation = true;
        yield return new WaitForSeconds(length);
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
