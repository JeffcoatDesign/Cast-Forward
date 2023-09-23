using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpellSystem;
public class PlayerSpells : MonoBehaviour
{
    public delegate void UpdatePlayerMana(float playerMana, float maxMana);
    public static event UpdatePlayerMana OnUpdatePlayerMana;
    public Spell leftSpell, rightSpell;
    [SerializeField] private float _maxMana;
    [SerializeField] private float _staminaRegenRate;
    private float _currentMana;
    [SerializeField] private Transform playerLeftTransform;
    [SerializeField] private Transform playerRightTransform;
    public void OnLeftSpell (InputAction.CallbackContext ctx)
    {
        bool canAffordSpell = _currentMana >= leftSpell.manaCost;
        if (canAffordSpell && ctx.performed && leftSpell != null)
        {
            _currentMana -= leftSpell.manaCost;
            leftSpell.SummonSpell(playerLeftTransform.position, playerLeftTransform.rotation, true);
            OnUpdatePlayerMana?.Invoke(_currentMana, _maxMana);
        }
    }
    public void OnRightSpell(InputAction.CallbackContext ctx)
    {
        bool canAffordSpell = _currentMana >= rightSpell.manaCost;
        if (canAffordSpell && ctx.performed && rightSpell != null)
        {
            _currentMana -= rightSpell.manaCost;
            rightSpell.SummonSpell(playerRightTransform.position, playerRightTransform.rotation, true);
            OnUpdatePlayerMana?.Invoke(_currentMana, _maxMana);
        }
    }
    private void FixedUpdate()
    {
        _currentMana = Mathf.Clamp(_currentMana + _staminaRegenRate, 0, _maxMana);
        OnUpdatePlayerMana?.Invoke(_currentMana, _maxMana);
    }
}
