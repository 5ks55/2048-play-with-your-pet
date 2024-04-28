using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Skins()
    {
        SceneManager.LoadSceneAsync(1);
    }
    public void Game()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
