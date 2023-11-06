using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EntityHealthBar : MonoBehaviour
{
    public Entity entity;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _text;
    private Transform _camTransform;
    bool _isAlive = true;
    private void OnEnable()
    {
        if (_text) _text.text = entity.name;
        entity.OnUpdateHP += UpdateHealth;
    }
    private void OnDisable()
    {
        entity.OnUpdateHP -= UpdateHealth;
    }
    void Update()
    {
        if (_isAlive)
        {
            transform.LookAt(_camTransform);
            transform.Rotate(new Vector3(0, 180, 0));
        }
    }
    void Start ()
    {
        _camTransform = Camera.main.transform;
    }
    void UpdateHealth (float current, float max)
    {
        _slider.maxValue = max;
        _slider.value = current;
        _isAlive = (current > 0);
        _slider.gameObject.SetActive(_isAlive);
        if (_text) _text.gameObject.SetActive(_isAlive);
    }
}