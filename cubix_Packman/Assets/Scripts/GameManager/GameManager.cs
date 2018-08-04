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
        PlayerPrefs.SetString("current_level", "level_2");
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
        CurrentLevel = LevelManager.GetCurrentLevel();
        FindObjectOfType<playerController>().Load();
        IsPlaying = true;
    }

    public static void GameWon()
    {
        LevelManager.NextLevel();
        IsPlaying = false;
        while(true)
            if (!FindObjectOfType<MazeBodyRotation>().rotate)
                break;
        Play();
    }
    
    /*if (GameManager.IsLevelCompleted)
    {
        game_level_loaded = false;
        Load();
        GameManager.IsLevelCompleted = false;
        GameManager.IsStart = true;
    }
    if (GameManager.IsStart)
    {
        transform.localPosition = GameManager.StartPosition;
        GameManager.IsStart = false;
        GameManager.IsPlaying = true;
        destination = transform.localPosition;
    }*/
    
}
