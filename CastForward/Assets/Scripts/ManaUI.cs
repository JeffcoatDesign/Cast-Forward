using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ManaUI : MonoBehaviour
{
    [SerializeField] private Slider manaSlider;
    private void OnEnable()
    {
        PlayerSpells.OnUpdatePlayerMana += UpdateMana;
    }
    private void OnDisable()
    {
        PlayerSpells.OnUpdatePlayerMana -= UpdateMana;
    }
    private void UpdateMana(float current, float max)
    {
        manaSlider.maxValue = max;
        manaSlider.value = current;
    }
}
