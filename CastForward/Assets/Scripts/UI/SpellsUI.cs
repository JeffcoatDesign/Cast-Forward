using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpellSystem;
using System;
using TMPro;

public class SpellsUI : MonoBehaviour
{
    [SerializeField] private Image _leftSpellImage;
    [SerializeField] private Image _rightSpellImage;
    [SerializeField] private Image _leftSpellCoolDown;
    [SerializeField] private Image _rightSpellCoolDown;
    [SerializeField] private TextMeshProUGUI _leftSpellChargeText;
    [SerializeField] private TextMeshProUGUI _rightSpellChargeText;
    private void OnEnable()
    {
        PlayerSpells.OnUpdatePlayerSpell += SetSpellSprites;
        PlayerSpells.OnUpdateRightSpellCooldown += UpdateRightCooldown;
        PlayerSpells.OnUpdateLeftSpellCooldown += UpdateLeftCooldown;
    }

    private void UpdateRightCooldown(float currentProgress, int charges, int maxCharges)
    {
        _rightSpellCoolDown.fillAmount = currentProgress;
        if (charges == maxCharges) {
            _rightSpellCoolDown.fillAmount = 1;
        }
        if (maxCharges > 1)
        {
            _rightSpellChargeText.text = charges.ToString();
        }
        else
            _rightSpellChargeText.text = "";
    }

    private void UpdateLeftCooldown(float currentProgress, int charges, int maxCharges)
    {
        _leftSpellCoolDown.fillAmount = currentProgress;
        if (charges == maxCharges) {
            _leftSpellCoolDown.fillAmount = 1;
        }
        if (maxCharges > 1)
        {
            _leftSpellChargeText.text = charges.ToString();
        }
        else
            _leftSpellChargeText.text = "";
    }

    private void OnDisable()
    {
        PlayerSpells.OnUpdatePlayerSpell -= SetSpellSprites;
        PlayerSpells.OnUpdateRightSpellCooldown -= UpdateRightCooldown;
        PlayerSpells.OnUpdateLeftSpellCooldown -= UpdateLeftCooldown;
    }
    void SetSpellSprites (Spell leftSpell, Spell rightSpell)
    {
        _leftSpellImage.enabled = (leftSpell != null);
        _rightSpellImage.enabled = (rightSpell != null);

        if (leftSpell != null)
            _leftSpellImage.sprite = leftSpell.inventorySprite;
        if (rightSpell != null)
            _rightSpellImage.sprite = rightSpell.inventorySprite;
    }
}
