using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadStory()
    {
        SceneManager.LoadScene("Story");
    }

    public void LoadMe()
    {
        SceneManager.LoadScene("Me");
    }
    public void LoadHome()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Close()
    {
        Application.Quit();
    }
}
