using UnityEngine;
using UnityEngine.SceneManagement;

public class TempMenu : MonoBehaviour
{
    public void LoadPlayground()
    {
        SceneManager.LoadScene("Playground");
    }
    public void LoadLevelOne()
    {
        SceneManager.LoadScene("Level1");
    }
}
