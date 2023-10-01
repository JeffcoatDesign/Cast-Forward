using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using SpellSystem;

public class SpellEquipMenu : MonoBehaviour
{
    [SerializeField] private float _spawnRadius = 3f;
    [SerializeField] private GameObject _pickupMenu;
    [SerializeField] private InputActionReference _leftSpellAction;
    [SerializeField] private InputActionReference _rightSpellAction;
    [SerializeField] private Image _spellToEquipImage;
    [SerializeField] private Image _leftSpellImage;
    [SerializeField] private Image _rightSpellImage;
    private PlayerSpells _playerSpells;
    private Spell currentSpell;
    bool _lookingForSpells = false;
    private void OnEnable()
    {
        Spell.OnSpellPickup += OnEquipSpell;
    }
    private void OnDisable()
    {
        Spell.OnSpellPickup -= OnEquipSpell;
    }
    void OnEquipSpell (Spell spell)
    {
        _playerSpells = FindFirstObjectByType<PlayerSpells>();
        _spellToEquipImage.sprite = spell.inventorySprite;
        _leftSpellImage.sprite = _playerSpells.leftSpell.inventorySprite;
        _rightSpellImage.sprite = _playerSpells.rightSpell.inventorySprite;
        currentSpell = spell;
        _lookingForSpells = true;
        _pickupMenu.SetActive(true);
        GameManager.instance.PauseGame(true);
    }
    private void Update()
    {
        if (_lookingForSpells)
        {
            float leftValue = _leftSpellAction.action.ReadValue<float>();
            float rightValue = _rightSpellAction.action.ReadValue<float>();
            if (leftValue > 0)
            {
                DropSpell(_playerSpells.leftSpell);
                _playerSpells.SetSpell(currentSpell, 0);
                _lookingForSpells = false;
                _pickupMenu.SetActive(false);
                GameManager.instance.PauseGame(false);
            }
            else if (rightValue > 0)
            {
                DropSpell(_playerSpells.rightSpell);
                _playerSpells.SetSpell(currentSpell, 1);
                _lookingForSpells = false;
                _pickupMenu.SetActive(false);
                GameManager.instance.PauseGame(false);
            }
        }
    }

    private void DropSpell (Spell spell)
    {
        Vector3 direction = _playerSpells.transform.forward * _spawnRadius;
        Vector3 spawnPos = _playerSpells.transform.position;
        spell.SpawnItem(spawnPos + direction);
    }
}
