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
        CheckPoint.ResetToStart();
        FindObjectOfType<SwipeControl>().Reset();
        FindObjectOfType<playerController>().Reset();
        FindObjectOfType<playerController>().Load();
        Debug.Log("THE LEVEL IS : " + CurrentLevel);
        
        IsGameWon = false;
        IsPlaying = true;
    }

    private void Update()
    {
        if (IsGameWon)
        {
            if (LevelManager.NextLevelPresent)
            {
                LevelTransitionUI.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
    }

    public static void GameWon()
    {
        Debug.Log("won the game");
        SaveManager sm = new SaveManager();
        LevelStatusSaveState state = new LevelStatusSaveState();
        state.LevelName =string.Format("level_{0}_{1}_" + "s", CurrentLevel.Split('_')[1], (int.Parse(CurrentLevel.Split('_')[2])).ToString());
        state.Load();
        Player.NoOfMoves = IndexOfCoinsCollected.Count;
        state.IndexOfCoinsCollected = state.IndexOfCoinsCollected.Union(IndexOfCoinsCollected).ToList();
        state.IndexOfDiamondsCollected = state.IndexOfDiamondsCollected.Union(IndexOfDiamondsCollected).ToList();
        state.Save();
                
        if(LevelManager.NextLevelPresent)
        {
            state.LevelName = string.Format("level_{0}_{1}_" + "s", CurrentLevel.Split('_')[1], (int.Parse(CurrentLevel.Split('_')[2]) + 1).ToString());
            state.Load();
            state.IsLocked = false;
            state.Save();
            
        }
        IsGameWon = true;
        IsPlaying = false;
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
