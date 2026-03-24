using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        if (musicSlider != null)
        {
            musicSlider.value = 1f;
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = 1f;
        }
    }

    public void OnMusicSliderChanged(float value)
    {
        if (GameManager.Instance == null || GameManager.Instance.AudioManager == null) return;
        GameManager.Instance.AudioManager.SetMusicVolume(value);
    }

    public void OnSFXSliderChanged(float value)
    {
        if (GameManager.Instance == null || GameManager.Instance.AudioManager == null) return;
        GameManager.Instance.AudioManager.SetSFXVolume(value);
    }
}