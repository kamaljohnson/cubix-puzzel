using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool IsGameOver;
    public static bool IsLevelCompleted;
    public static bool IsPlaying;
    public static bool IsStart;
    
    public static Vector3 EndPosition = new Vector3();
    public static Vector3 StartPosition = new Vector3();
    
    /*public GameObject PlayUi;
    public GameObject SettingsUi;
    public GameObject GameOverUi;
    public GameObject LevelSelectionUi;*/
    
    public static string CurrentLevel;
    
    private void Start()
    {
        PlayerPrefs.SetString("current_level", "level_1");
        CurrentLevel = LevelManager.GetCurrentLevel();
        Play();
    }

    public static void GameOver()
    {
        
    }

    public static void Settings()
    {
        
    }

    public static void Play()
    {
        FindObjectOfType<SwipeControl>().Reset();
        FindObjectOfType<playerController>().Reset();
        FindObjectOfType<playerController>().Load();
        IsPlaying = true;
    }

    public static void GameWon()
    {
        LevelManager.NextLevel();
        CurrentLevel = LevelManager.GetCurrentLevel();
        Play();
    }
}
