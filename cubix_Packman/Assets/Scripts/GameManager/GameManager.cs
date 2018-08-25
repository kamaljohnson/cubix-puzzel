using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static bool IsGameOver;
    public static bool IsLevelCompleted;
    public static bool IsPlaying;
    public static bool IsStart;
    public static bool IsGameWon;
    
    public static Vector3 EndPosition = new Vector3();
    public static Vector3 StartPosition = new Vector3();
    public static GameObject EndState;
    
    public static List<int> IndexOfCoinsCollected = new List<int>(); 
    public static List<int> IndexOfDiamondsCollected = new List<int>();

    public GameObject LevelTransitionUI;
    
    public static string CurrentLevel;
    
    private void Start()
    {
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
        IsGameWon = false;
        FindObjectOfType<SwipeControl>().Reset();
        FindObjectOfType<playerController>().Reset();
        FindObjectOfType<playerController>().Load();
        IsGameWon = false;
        IsPlaying = true;
    }

    private void Update()
    {
        if(IsGameWon)
            LevelTransitionUI.SetActive(true);
    }

    public static void GameWon()
    {
        /*SaveManager sm = new SaveManager();
        LevelStatusSaveState state = new LevelStatusSaveState();
        state.LevelName = CurrentLevel;
        state.Load();
        Player.NoOfMoves = IndexOfCoinsCollected.Count;
        state.IndexOfCoinsCollected = state.IndexOfCoinsCollected.Union(IndexOfCoinsCollected).ToList();
        state.IndexOfDiamondsCollected = state.IndexOfDiamondsCollected.Union(IndexOfDiamondsCollected).ToList();
        state.Save();
        state.LevelName = "level_" + CurrentLevel.Split('_')[1] + "_" + (int.Parse(CurrentLevel.Split('_')[1]) + 1).ToString();
        state.Load();
        state.IsLocked = false;
        state.Save();*/
        IsGameWon = true;
        IsPlaying = false;
        PlayerPrefs.SetInt("levelIndex", PlayerPrefs.GetInt("levelIndex") + 1);
    }

    public void LevelTransitionOnRestart()
    {
        LevelTransitionUI.SetActive(false);
        Play();
    }

    public void LevelTransionOnHome()
    {
        LevelTransitionUI.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LevelTransionOnNext()
    {
        LevelTransitionUI.SetActive(false);
        LevelManager.NextLevel();        
    }
    public static void LoadGameScene()
    {
        Debug.Log("opened the game scene with level " + CurrentLevel);
        SceneManager.LoadScene("Game");

    }
}
