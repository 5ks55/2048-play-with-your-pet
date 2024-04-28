using TMPro;
using UnityEngine;

public class StartLoseBackground : MonoBehaviour
{
    private GameObject[] available;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private int lossCounter;

    private void Start()
    { 
        available = new GameObject[transform.childCount];
        lossCounter = PlayerPrefs.GetInt("LossCounter", 0);

        for (int i = 0; i < transform.childCount; i++)
        {
            available[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in available)
        {
            go.SetActive(false);
        }

        if (lossCounter >= 150)
        {
            available[0].SetActive(true);
        }
        else
        {
            available[1].SetActive(true);
            descriptionText.text += $"\n{lossCounter}/150";
        }
    }
}
