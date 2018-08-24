using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static bool IsGameOver;
    public static bool IsLevelCompleted;
    public static bool IsPlaying;
    public static bool IsStart;
    
    public static Vector3 EndPosition = new Vector3();
    public static Vector3 StartPosition = new Vector3();
    public static GameObject EndState;

    
    public static string CurrentLevel = "level_1_1";
    
    private void Start()
    {
        if(LevelManager.CurrentLevel != "")
            CurrentLevel = LevelManager.CurrentLevel;
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
        PlayerPrefs.SetInt(CurrentLevel + "_Points", playerController.PointsCollected);
        LevelManager.NextLevel();
        //TODO go back to the level map after winning a level
        Play();
    }

    public static void LoadGameScene()
    {
        Debug.Log("opened the game scene with level " + CurrentLevel);
        SceneManager.LoadScene("Game");

    }
}
