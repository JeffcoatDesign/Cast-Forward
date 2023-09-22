using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using SpellSystem;
public class PlayerSpells : MonoBehaviour
{
    public Spell leftSpell, rightSpell;
    [SerializeField] private Transform playerLeftTransform;
    [SerializeField] private Transform playerRightTransform;
    public void OnLeftSpell (InputAction.CallbackContext ctx)
    {
        if (ctx.performed && leftSpell != null)
            leftSpell.SummonSpell(playerLeftTransform.position, playerLeftTransform.rotation, true);
    }
    public void OnRightSpell(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && rightSpell != null)
            rightSpell.SummonSpell(playerRightTransform.position, playerRightTransform.rotation, true);
    }
}
