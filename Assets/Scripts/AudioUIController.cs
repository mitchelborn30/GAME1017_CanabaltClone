using UnityEngine;
using UnityEngine.UI;

public class AudioUIController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;

    private void Start()
    {
        if (musicSlider != null)
        {
            musicSlider.value = 1f;
        }
    }

    public void OnMusicSliderChanged(float value)
    {
        if (GameManager.Instance == null || GameManager.Instance.AudioManager == null) return;
        GameManager.Instance.AudioManager.SetMusicVolume(value);
    }
}