using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject menuObj;
    private void OnEnable()
    {
        PlayerEntity.OnPlayerDeath += OnDeath;
    }
    private void OnDisable()
    {
        PlayerEntity.OnPlayerDeath -= OnDeath;
    }
    void OnDeath ()
    {
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.instance.Pause(true);
        menuObj.SetActive(true);
    }
    public void GoToMenu ()
    {
        if (GameManager.instance != null) GameManager.instance.transform.parent = transform;
        SceneManager.LoadScene("Menu");
    }
}
