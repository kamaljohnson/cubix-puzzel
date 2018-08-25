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
        IsGameWon = false;
        FindObjectOfType<SwipeControl>().Reset();
        FindObjectOfType<playerController>().Reset();
        FindObjectOfType<playerController>().Load();
        IsPlaying = true;
    }

    private void Update()
    {
        if(IsGameWon)
            LevelTransitionUI.SetActive(true);
    }

    public static void GameWon()
    {
        IsGameWon = true;
        SaveManager sm = new SaveManager();
        SaveState state = sm.Load(CurrentLevel);
        state.IndexOfCoinsCollected = state.IndexOfCoinsCollected.Union(IndexOfCoinsCollected).ToList();
        state.IndexOfDiamondsCollectd = state.IndexOfDiamondsCollectd.Union(IndexOfDiamondsCollected).ToList();
        sm.Save(state);
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
