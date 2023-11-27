using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpellSystem;

public class EnemySpells : MonoBehaviour
{
    public Spell[] enemySpells;
    private Spell selectedSpell;
    private EnemyAI enemyAI;
    [SerializeField] private Transform spellTransform;
    [SerializeField] private float _maxMana = 100f;
    [SerializeField] private float _manaRegenSpeed = 0.1f;
    private Rigidbody _rb;
    private float _currentMana;
    private void Awake()
    {
        enemyAI = GetComponent<EnemyAI>();
        _rb = GetComponent<Rigidbody>();
        _currentMana = _maxMana;   
        SelectSpell();
    }
    public void OnSpellAttack()
    {
        selectedSpell.SummonSpell(spellTransform, _rb.velocity, false);
        //_currentMana -= selectedSpell.manaCost;
        SelectSpell();
    }
    void SelectSpell()
    {
        if (enemySpells.Length < 1) return;
        //Spell[] affordableSpells = enemySpells.Where(sp => sp.manaCost <= _currentMana).ToArray();
        //if (affordableSpells.Length < 1) return;
        //int randomIndex = Random.Range(0, affordableSpells.Length);
        //selectedSpell = affordableSpells[randomIndex];
        enemyAI.projectileForce = selectedSpell.ProjectileSpeed;
    }
    private void FixedUpdate()
    {
        _currentMana = Mathf.Clamp(_currentMana + _manaRegenSpeed, 0, _maxMana);
    }
}
