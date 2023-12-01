using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpellSystem;
using Unity.VisualScripting;

public class EnemySpells : MonoBehaviour
{
    [SerializeField] private Spell spell;
    private EnemyAI enemyAI;
    [SerializeField] private Transform spellTransform;
    private Rigidbody _rb;
    private PlayerController _playerController;
    private EnemyStateMachine _enemyStateMachine;
    private int charges;
    private int maxCharges;
    private float coolDownTime;
    bool canCast = true;
    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        _rb = GetComponent<Rigidbody>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _playerController = FindFirstObjectByType<PlayerController>();
        if (_enemyStateMachine != null)
        {
            _enemyStateMachine.IsMelee = false;
        }
        charges = spell.charges;
        maxCharges = spell.charges;
    }
    public void OnSpellAttack()
    {
        if (!canCast && charges < 1) return;
        
        float castTime = spell.castingTime;
        _enemyStateMachine.StartTrigger("Cast",true,castTime,"castSpeed");
        StartCoroutine(PauseCasting(castTime));
    }
    public void CastSpell ()
    {
        spell.SummonSpell(spellTransform, _rb.velocity, false);
    }
    private IEnumerator PauseCasting (float castTime)
    {
        canCast = false;
        yield return new WaitForSeconds(castTime);
        canCast = true;
        yield return null;
    }
    private void Update()
    {
        if (_playerController != null)
            spellTransform.LookAt(_playerController.transform.position);
    }
}
