using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Speed : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Rigidbody rb;

    // Update is called once per frame
    void Update()
    {
        if (text != null && rb != null)
            text.text = rb.velocity.magnitude.ToString();
    }
}
