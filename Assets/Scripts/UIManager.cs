using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text finalTimeText;
    [SerializeField] private TMP_Text highScoreText;  
    [SerializeField] private TMP_Text leaderboardText;  

  

    private void Start()
    {
        if (finalTimeText != null && GameManager.Instance != null)
        {
            finalTimeText.text = "Final Time: " + GameManager.Instance.FinalTime.ToString("F2");
        }
        if (highScoreText != null)  
        {
            highScoreText.text = "High Score: " + GameManager.Instance.HighScore.ToString("F2");  
        }

        UpdateLeaderboardUI();  
    }



    private void Update()
    {
        if (timerText != null && GameManager.Instance != null)
        {
            timerText.text = "Time: " + GameManager.Instance.CurrentRunTime.ToString("F2");
        }

    }

    private void UpdateLeaderboardUI()  
    {
        if (leaderboardText == null) return;  

        List<float> scores = SaveSystem.GetLeaderboard();  

        if (scores.Count == 0)  
        {
            leaderboardText.text = "Leaderboard:\nNo scores yet";  
            return;  
        }

        string display = "Leaderboard:\n";  

        for (int i = 0; i < scores.Count; i++)  
        {
            display += (i + 1) + ". " + scores[i].ToString("F2") + "\n";  
        }

        leaderboardText.text = display;  
    }

    public void OnStartButtonPressed()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.StartGame();
    }

    public void OnRestartButtonPressed()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.RestartGame();
    }

    public void OnReturnToTitlePressed()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.LoadTitleScene();
    }

    public void OnQuitButtonPressed()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.QuitGame();
    }
}