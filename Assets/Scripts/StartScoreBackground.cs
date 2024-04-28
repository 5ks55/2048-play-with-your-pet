using TMPro;
using UnityEngine;

public class StartScoreBackground : MonoBehaviour
{
    private GameObject[] available;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private int hiscore;

    private void Start()
    { 
        available = new GameObject[transform.childCount];
        hiscore = PlayerPrefs.GetInt("hiscore", 0);

        for (int i = 0; i < transform.childCount; i++)
        {
            available[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in available)
        {
            go.SetActive(false);
        }

        if (hiscore >= 10000)
        {
            available[0].SetActive(true);
        }
        else
        {
            available[1].SetActive(true);
            descriptionText.text += $"\n{hiscore}/10000";
        }

    }
}
