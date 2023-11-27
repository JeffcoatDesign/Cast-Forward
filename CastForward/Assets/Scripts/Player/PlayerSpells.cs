using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpellSystem;
public class PlayerSpells : MonoBehaviour
{
    public delegate void UpdatePlayerSpell(Spell leftSpell, Spell rightSpell);
    public delegate void UpdatePlayerSpellCooldown(float currentProgress, int charges, int maxCharges);
    public static event UpdatePlayerSpell OnUpdatePlayerSpell;
    public static event UpdatePlayerSpellCooldown OnUpdateLeftSpellCooldown;
    public static event UpdatePlayerSpellCooldown OnUpdateRightSpellCooldown;
    public Spell leftSpell, rightSpell;
    [SerializeField] private InputActionReference _leftActionReference;
    [SerializeField] private InputActionReference _rightActionReference;
    [SerializeField] private Transform playerLeftTransform;
    [SerializeField] private Transform playerRightTransform;
    [SerializeField] private Rigidbody _rb;
    private bool _canCastLeft = true;
    private bool _canCastRight = true;

    private float leftSpellProgress;
    private int leftSpellCharges;
    private float rightSpellProgress;
    private int rightSpellCharges;
    private bool leftWasHeld = false;
    private bool rightWasHeld = false;
    private void Start()
    {
        OnUpdatePlayerSpell?.Invoke(leftSpell, rightSpell);
    }
    private void Update()
    {
        if (_leftActionReference.action.IsPressed())
            OnLeftSpell();
        else
            leftWasHeld = false;
        if (_rightActionReference.action.IsPressed())
            OnRightSpell();
        else 
            rightWasHeld = false;

        if (leftSpell && leftSpellCharges < leftSpell.charges)
        {
            leftSpellProgress += Time.deltaTime;
            if (leftSpellProgress >= leftSpell.coolDown) {
                leftSpellCharges++;
                leftSpellProgress = 0;
            }
            OnUpdateLeftSpellCooldown?.Invoke(leftSpellProgress/leftSpell.coolDown, leftSpellCharges, leftSpell.charges);
        }
        if (rightSpell && rightSpellCharges < rightSpell.charges)
        {
            rightSpellProgress += Time.deltaTime;
            if (rightSpellProgress >= rightSpell.coolDown) {
                rightSpellCharges++;
                rightSpellProgress = 0;
            }
            OnUpdateRightSpellCooldown?.Invoke(rightSpellProgress/rightSpell.coolDown, rightSpellCharges, rightSpell.charges);
        }
    }
    void OnLeftSpell ()
    {
        if (GameManager.instance.IsPaused || leftSpell == null) return;
        if (!leftSpell.canBeHeld && leftWasHeld == true) return;
        if (_canCastLeft && leftSpellCharges > 0 && leftSpell != null)
        {
            _canCastLeft = false;
            Invoke(nameof(DelayLeftSpell), leftSpell.castDelay);
            leftSpellCharges--;
            leftSpell.SummonSpell(playerLeftTransform, _rb.velocity, true);
        }
        leftWasHeld = true;
    }
    void OnRightSpell()
    {
        if (GameManager.instance.IsPaused || rightSpell == null) return;
        if (!rightSpell.canBeHeld && rightWasHeld == true) return;
        if (_canCastRight && rightSpellCharges > 0 && rightSpell != null)
        {
            _canCastRight = false;
            Invoke(nameof(DelayRightSpell), rightSpell.castDelay);
            rightSpellCharges--;
            rightSpell.SummonSpell(playerRightTransform, _rb.velocity, true);
        }
        rightWasHeld = true;
    }
    public void SetSpell (Spell spell, int slot)
    {
        if (slot == 0)
        {
            leftSpell = spell;
            leftSpellCharges = leftSpell.charges;
            leftSpellProgress = 0f;
            OnUpdateLeftSpellCooldown?.Invoke(leftSpellProgress / leftSpell.coolDown, leftSpellCharges, leftSpell.charges);
        }
        else if (slot == 1)
        {
            rightSpell = spell;
            rightSpellCharges = rightSpell.charges;
            rightSpellProgress = 0f;
            OnUpdateRightSpellCooldown?.Invoke(rightSpellProgress/rightSpell.coolDown, rightSpellCharges, rightSpell.charges);
        }
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
}
