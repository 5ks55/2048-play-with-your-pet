using UnityEngine;
using Gley.GameServices;

public class ButtonManager : MonoBehaviour
{
    public GameObject settings;
    public GameObject startPanel;
    public GameObject restart;

    [SerializeField] private TileBoard board;
    [SerializeField] private GameObject achievements;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject skins;
    [SerializeField] private GameObject paws;

    public void Settings()
    {
        if (!settings.activeSelf)
        {
            settings.SetActive(true);
            board.enabled = false;
            if (!startPanel.activeSelf)
            {
                paws.SetActive(true);
                achievements.SetActive(true);
                leaderboard.SetActive(true);
                skins.SetActive(true);
            }
        }
        else if (settings.activeSelf)
        {
            settings.SetActive(false);
            board.enabled = true;
            if (!startPanel.activeSelf)
            {
                paws.SetActive(false);
                achievements.SetActive(false);
                leaderboard.SetActive(false);
                skins.SetActive(false);
            }
        }
    }

    public void LeaderBoardButton()
    {
        Login();
        Gley.GameServices.API.ShowLeaderboadsUI();
    }

    public void AchiButton()
    {
        Login();
        Gley.GameServices.API.ShowAchievementsUI();
    }

    public void StartPanel()
    {
        board.enabled = true;
        restart.SetActive(true);
        achievements.SetActive(false);
        leaderboard.SetActive(false);
        skins.SetActive(false);
        startPanel.SetActive(false);
    }

    public void Login()
    {
        Gley.GameServices.API.LogIn();
    }
}
