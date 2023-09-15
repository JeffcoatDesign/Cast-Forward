using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Speed : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Rigidbody rb;
// TODO: DELETE THIS FROM PROJECT EVENTUALLY
    void Update()
    {
        if (text != null && rb != null)
            text.text = rb.velocity.magnitude.ToString();
    }
}
