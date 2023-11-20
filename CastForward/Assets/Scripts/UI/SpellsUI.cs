using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpellSystem;

public class SpellsUI : MonoBehaviour
{
    [SerializeField] private Image _leftSpellImage;
    [SerializeField] private Image _rightSpellImage;
    private void OnEnable()
    {
        PlayerSpells.OnUpdatePlayerSpell += SetSpellSprites;
    }
    private void OnDisable()
    {
        PlayerSpells.OnUpdatePlayerSpell -= SetSpellSprites;
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
