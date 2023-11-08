using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        AudioManager.Instance.PlaySong("Audio/Path to Lake Land");
    }
    public void LoadPlayground()
    {
        SceneManager.LoadScene("Playground");
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }
}
