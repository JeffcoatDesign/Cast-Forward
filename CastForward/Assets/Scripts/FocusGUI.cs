using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FocusGUI : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        PlayerController.OnSetFocus += SetFocus;
    }
    private void OnDisable()
    {
        PlayerController.OnSetFocus -= SetFocus;
    }
    private void SetFocus(Interactable focus)
    {
        if (focus != null)
        {
            _text.text = focus.Verb + "\n" + focus.InteractableName;
        }
        else _text.text = "";
    }
}
