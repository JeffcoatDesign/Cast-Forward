using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SpellSystem;

public class EnemySpells : MonoBehaviour
{
    public Spell[] enemySpells;
    [SerializeField] private Transform spellTransform;
    [SerializeField] private float _maxMana = 100f;
    [SerializeField] private float _manaRegenSpeed = 0.1f;
    private float _currentMana;
    private void Awake()
    {
        _currentMana = _maxMana;
    }
    public void OnSpellAttack()
    {
        if (enemySpells.Length < 1) return;
        Spell[] affordableSpells = enemySpells.Where(sp => sp.manaCost <= _currentMana).ToArray();
        if (affordableSpells.Length < 1) return;
        int randomIndex = Random.Range(0, affordableSpells.Length);
        Spell selectedSpell = affordableSpells[randomIndex];
        _currentMana -= selectedSpell.manaCost;
        selectedSpell.SummonSpell(spellTransform.position, spellTransform.rotation, false);
    }
    private void FixedUpdate()
    {
        _currentMana = Mathf.Clamp(_currentMana + _manaRegenSpeed, 0, _maxMana);
    }
}
