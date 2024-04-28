using UnityEngine;

public class SelectedBackground : MonoBehaviour
{
    private GameObject[] backgrounds;
    private int index;

    private void Start()
    {
        index = PlayerPrefs.GetInt("SelectBackground");
        backgrounds = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
        }

        if (index >= 0 && index < backgrounds.Length)
        {
            foreach (GameObject go in backgrounds)
            {
                go.SetActive(false);
            }

            backgrounds[index].SetActive(true);
        }
        else
        {
            backgrounds[0].SetActive(true);
        }
    }
}