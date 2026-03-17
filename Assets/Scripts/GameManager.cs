using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Managers")]
    [SerializeField] private AudioManager audioManager;

    [Header("Scene Names")]
    [SerializeField] private string titleSceneName = "TitleScene";
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private string gameOverSceneName = "GameOverScene";

    private float runTimer = 0f;
    private float finalTime = 0f;
    private bool timerRunning = false;

    public AudioManager AudioManager => audioManager;
    public float CurrentRunTime => runTimer;
    public float FinalTime => finalTime;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioManager == null)
        {
            audioManager = GetComponent<AudioManager>();
        }
    }

    private void Update()
    {
        if (timerRunning)
        {
            runTimer += Time.deltaTime;
        }
    }

    public void StartGame()
    {
        runTimer = 0f;
        finalTime = 0f;
        timerRunning = true;
        SceneManager.LoadScene(gameSceneName);
    }

    public void GameOver()
    {
        timerRunning = false;
        finalTime = runTimer;
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void RestartGame()
    {
        runTimer = 0f;
        finalTime = 0f;
        timerRunning = true;
        SceneManager.LoadScene(gameSceneName);
    }

    public void LoadTitleScene()
    {
        timerRunning = false;
        SceneManager.LoadScene(titleSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
