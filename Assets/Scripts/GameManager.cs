using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TileBoard board;
    [SerializeField] private CanvasGroup gameOver;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI hiscoreText;
    [SerializeField] private TextMeshProUGUI startText;

    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private Slider slider;
    [SerializeField] private GPAchievements achievements;
    [SerializeField] private InterstitialAds interstitialAds;
    
    private int score;
    private int lossCounter;
    private int dataVersion = 1;

    private void Awake()
    {
        int savedDataVersion = PlayerPrefs.GetInt("DataVersion", -1);

        if (savedDataVersion != dataVersion)
        {
            ClearExistingData();
            PlayerPrefs.SetInt("DataVersion", dataVersion);
        }
    }

    private void ClearExistingData()
    {
        PlayerPrefs.SetInt("score", 0);
        board.DeleteData();
    }

    private void Start()
    {
        slider.value = PlayerPrefs.GetFloat("BG_MUSIC",1);
        Game();
    }


    public void NewGame()
    {
        board.DeleteData();
        PlayerPrefs.SetInt("score", 0);
        Game();
        buttonManager.restart.SetActive(true);
        if (buttonManager.settings.activeSelf)
        {
            buttonManager.Settings();
        }
    }
    public void Game()
    { 
        board.LoadDataIndicator();
        SetScore(PlayerPrefs.GetInt("score", 0));

        hiscoreText.text = LoadHiscore().ToString();
        lossCounter = PlayerPrefs.GetInt("LossCounter", 0);
        achievements.LoseAchievements(PlayerPrefs.GetInt("LossCounter", 0));

        gameOver.alpha = 0f;
        gameOver.interactable = false;
        
        
        board.ClearBoard();


        if (!board.dataSaved)
        {
            startText.text = "Swipe To Start!";
            board.CreateTile();
            board.CreateTile();
        }
        else if (board.dataSaved && board != null)
        {
            startText.text = "Swipe To Continue!";
            board.LoadData();
        }
        else 
        {
            startText.text = "Swipe To Start!";
            board.CreateTile();
            board.CreateTile();
        }

        if (buttonManager.startPanel.activeSelf)
        {
            board.enabled = false;
        }
        else if (!buttonManager.startPanel.activeSelf)
        {
            board.enabled = true;
        }
        
    }

    public void GameOver()
    {
        lossCounter++;
        PlayerPrefs.SetInt("LossCounter", lossCounter);

        if (Application.internetReachability != NetworkReachability.NotReachable)
            achievements.LoseAchievements(PlayerPrefs.GetInt("LossCounter", 0));

        PlayerPrefs.SetInt("score", 0);
        board.DeleteData();
        board.enabled = false; 
        gameOver.interactable = true; 
        buttonManager.restart.SetActive(false);
        StartCoroutine(Fade(gameOver, 1f, 0.5f));
        StartCoroutine(ShowInterstitialWithDelay(1.0f));
    }

    private IEnumerator ShowInterstitialWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        interstitialAds.Show();
    }


    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void IncreaseScore(int points)
    {
        SetScore(score + points);
        PlayerPrefs.SetInt("score", score);
    }

    private void SetScore(int score)
    {
        this.score = score;

        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
            hiscoreText.text = score.ToString();
        }

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            Gley.GameServices.API.SubmitScore(PlayerPrefs.GetInt("hiscore", 0), Gley.GameServices.LeaderboardNames.PuzzleWizards);
            achievements.ScoreAchievements(PlayerPrefs.GetInt("hiscore", 0));
        }
    }

    private int LoadHiscore()
    {      
        return PlayerPrefs.GetInt("hiscore", 0);
    }

}
