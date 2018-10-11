using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static string CurrentLevel = "";
    private static bool _created = false;
    public GameObject LevelPlayCard;
    public static bool NextLevelPresent;
    
    void Awake()
    {
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
    }

    public static void ActivateLevel()
    {
        GameManager.CurrentLevel = CurrentLevel;
        Debug.Log("level : " + CurrentLevel);
        
        
        var intermediateLevelStringList = CurrentLevel.Split('_');
        
        var level = int.Parse(intermediateLevelStringList[2]) + 1;
        if (level > SeasonManager.NumberOfLevelsInCurrentSeason)
        {
            Debug.Log("NEXT LEVEL NOT PRESENT");
            NextLevelPresent = false;
        }
        else
        {
            
            Debug.Log("NEXT LEVEL PRESENT");
            NextLevelPresent = true;
        }
        
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void BackButtonOnClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public static void NextLevel()
    {
        var intermediateLevelStringList = CurrentLevel.Split('_');
        
        var level = int.Parse(intermediateLevelStringList[2]) + 1;
        if (level > SeasonManager.NumberOfLevelsInCurrentSeason)
        {
            NextLevelPresent = false;
        }
        else
        {
            CurrentLevel = string.Format("{0}_{1}_{2}_{3}", intermediateLevelStringList[0], intermediateLevelStringList[1], level.ToString(), "L");
            GameManager.CurrentLevel = CurrentLevel;
            GameManager.Play();
        }
    }

}
