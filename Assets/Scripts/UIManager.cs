using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text finalTimeText;

    private void Update()
    {
        if (timerText != null && GameManager.Instance != null)
        {
            timerText.text = "Time: " + GameManager.Instance.CurrentRunTime.ToString("F2");
        }
    }

    private void Start()
    {
        if (finalTimeText != null && GameManager.Instance != null)
        {
            finalTimeText.text = "Final Time: " + GameManager.Instance.FinalTime.ToString("F2");
        }
    }

    public void OnStartButtonPressed()
    {
        GameManager.Instance.StartGame();
    }

    public void OnRestartButtonPressed()
    {
        GameManager.Instance.RestartGame();
    }

    public void OnReturnToTitlePressed()
    {
        GameManager.Instance.LoadTitleScene();
    }

    public void OnMusicSliderChanged(float value)
    {
        GameManager.Instance.AudioManager.SetMusicVolume(value);
    }

    public void OnSFXSliderChanged(float value)
    {
        GameManager.Instance.AudioManager.SetSFXVolume(value);
    }

    public void OnQuitButtonPressed()
    {
        GameManager.Instance.QuitGame();
    }

}
