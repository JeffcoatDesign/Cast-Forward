using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpellSystem;
public class PlayerSpells : MonoBehaviour
{
    public delegate void UpdatePlayerSpell(Spell leftSpell, Spell rightSpell);
    public delegate void UpdatePlayerMana(float playerMana, float maxMana);
    public static event UpdatePlayerMana OnUpdatePlayerMana;
    public static event UpdatePlayerSpell OnUpdatePlayerSpell;
    public Spell leftSpell, rightSpell;
    [SerializeField] private float _maxMana;
    [SerializeField] private float _staminaRegenRate;
    [SerializeField] private InputActionReference _leftActionReference;
    [SerializeField] private InputActionReference _rightActionReference;
    private float _currentMana;
    [SerializeField] private Transform playerLeftTransform;
    [SerializeField] private Transform playerRightTransform;
    private bool _canCastLeft = true;
    private bool _canCastRight = true;
    private void Start()
    {
        _currentMana = _maxMana;
        OnUpdatePlayerSpell?.Invoke(leftSpell, rightSpell);
    }
    private void Update()
    {
        if (_leftActionReference.action.IsPressed())
            OnLeftSpell();
        if (_rightActionReference.action.IsPressed())
            OnRightSpell();
    }
    void OnLeftSpell ()
    {
        if (GameManager.instance.IsPaused) return;
        bool canAffordSpell = _currentMana >= leftSpell.manaCost;
        if (_canCastLeft && canAffordSpell && leftSpell != null)
        {
            _canCastLeft = false;
            Invoke(nameof(DelayLeftSpell), leftSpell.castDelay);
            _currentMana -= leftSpell.manaCost;
            leftSpell.SummonSpell(playerLeftTransform, true);
            OnUpdatePlayerMana?.Invoke(_currentMana, _maxMana);
        }
    }
    void OnRightSpell()
    {
        if (GameManager.instance.IsPaused) return;
        bool canAffordSpell = _currentMana >= rightSpell.manaCost;
        if (_canCastRight && canAffordSpell && rightSpell != null)
        {
            _canCastRight = false;
            Invoke(nameof(DelayRightSpell), rightSpell.castDelay);
            _currentMana -= rightSpell.manaCost;
            rightSpell.SummonSpell(playerRightTransform, true);
            OnUpdatePlayerMana?.Invoke(_currentMana, _maxMana);
        }
    }
    public void SetSpell (Spell spell, int slot)
    {
        if (slot == 0) leftSpell = spell;
        else if (slot == 1) rightSpell = spell;
        OnUpdatePlayerSpell?.Invoke(leftSpell, rightSpell);
    }
    void DelayLeftSpell ()
    {
        _canCastLeft = true;
    }
    void DelayRightSpell ()
    {
        _canCastRight = true;
    }
    private void FixedUpdate()
    {
        _currentMana = Mathf.Clamp(_currentMana + _staminaRegenRate, 0, _maxMana);
        OnUpdatePlayerMana?.Invoke(_currentMana, _maxMana);
    }
}
