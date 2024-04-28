using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundSelect : MonoBehaviour
{
    private GameObject[] backgrounds;
    private int index;

    void Start()
    {
        index = PlayerPrefs.GetInt("SelectBackground");
        backgrounds = new GameObject[transform.childCount];
    }

    public void SelectLeft()
    {
        backgrounds[index].SetActive(false);
        index--;
        if (index < 0)
        {
            index = backgrounds.Length - 1;
        }
        backgrounds[index].SetActive(true);
    }

    public void SelectRight()
    {
        backgrounds[index].SetActive(false);
        index--;
        if (index == backgrounds.Length)
        {
            index = 0;
        }
        backgrounds[index].SetActive(true);
    }

    public void StartScene()
    {
        PlayerPrefs.SetInt("SelectBackground", index);
        SceneManager.LoadSceneAsync(0);
    }
}
