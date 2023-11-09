using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class EnterEventTrigger : MonoBehaviour {
    public List<string> targetTags;
    public UnityEvent OnTriggered;
    void Start() {
        if (OnTriggered == null) OnTriggered = new();
    }

    private void OnTriggerEnter(Collider other) {
        if (targetTags.Count <= 0) return;
        foreach (string tag in targetTags) {
            if (other.CompareTag(tag)) {
                OnTriggered.Invoke();
                return;
            }
        }
    }
}
