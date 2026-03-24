using UnityEngine;

public class PersistentAudioUI : MonoBehaviour
{
    private static PersistentAudioUI instance;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

    }
}