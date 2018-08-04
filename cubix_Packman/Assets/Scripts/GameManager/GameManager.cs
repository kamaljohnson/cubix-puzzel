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
    
    public static string CurrentLevel = "level_0";
    
    private void Start()
    {
       IsLevelCompleted = true;
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
    }

    public static void GameWon()
    {
        LevelManager.NextLevel();
    }
    
}
