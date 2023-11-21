using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuUI : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        if (GameManager.instance != null) GameManager.instance.transform.parent = transform;
    }

    public void GoToMenu ()
    {
        SceneManager.LoadScene("Menu");
    }
}
