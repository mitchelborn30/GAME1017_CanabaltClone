using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mainMixer;

   // public AudioSource musicTrack1;
   //  public AudioSource sfxSound1;

    public void SetTotalVolume(float volume)
    {
        mainMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        mainMixer.SetFloat("VolumeMusic", volume);
    }

   // private void Start()
    //{
      //  musicTrack1 = GameObject.Find("Track0").GetComponent<AudioSource>();
    //}
}
