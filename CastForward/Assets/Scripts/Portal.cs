using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            EnterPortal();
    }
    public override void Interact()
    {
        EnterPortal();
    }
    private void EnterPortal()
    {
        SceneManager.LoadScene(LevelGenerator.instance.nextLevel);
    }
}
