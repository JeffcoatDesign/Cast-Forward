using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Schema;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private void OnEnable()
    {
        PlayerEntity.OnPlayerHPChange += UpdateHealth;
    }
    private void OnDisable()
    {
        PlayerEntity.OnPlayerHPChange -= UpdateHealth;
    }
    private void UpdateHealth(float current, float maxHitpoints)
    {
        healthSlider.maxValue = maxHitpoints;
        healthSlider.value = current;
    }
}
