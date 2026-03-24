using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{

    [Header("UI References")]
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

    /*public void OnMasterSliderChanged(float value)
    {
        if (GameManager.Instance == null || GameManager.Instance.AudioManager == null)
        {
            Debug.LogError("AudioManager is NULL");
            return;
        }

        GameManager.Instance.AudioManager.SetMasterVolume(value);
    }

    public void OnMusicSliderChanged(float value)
    {
        if (GameManager.Instance == null || GameManager.Instance.AudioManager == null)
        {
            Debug.LogError("AudioManager is NULL");
            return;
        }

        GameManager.Instance.AudioManager.SetMusicVolume(value);
    }*/
}