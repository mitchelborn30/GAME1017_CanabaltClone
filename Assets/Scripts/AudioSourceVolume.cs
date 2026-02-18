using UnityEngine;

public class MySingleton
{ 
    MySingleton() { }

    private static MySingleton instance;

    public static MySingleton GetInstance()
    { 
        if (instance == null)
        {
            instance = new MySingleton();
        }
        return instance; 
    }
}
public class AudioSourceVolume : MonoBehaviour
{
    public AudioSource audio;
    void Start()
    {
        audio = GetComponent<AudioSource>();
      //  audio.volume = 0.6f;

     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        audio.volume = volume;

        Input.GetKeyDown(KeyCode.Space);
    }
}
