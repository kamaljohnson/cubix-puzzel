using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class StartManager : MonoBehaviour
{

    public List<int> NumberOfLevelsInSeasons;
    
    void Start()
    {
        SeasonManager.NumberOfLevelsInSeasons = NumberOfLevelsInSeasons;
        
        PlayerPrefs.SetString("gameStatus", "NotSet");
        if (PlayerPrefs.GetString("gameStatus") == "NotSet")
        {
            PlayerPrefs.SetString("gameStaus", "Set");
            // set all the level status to initial values
            for (int i = 1; i <= NumberOfLevelsInSeasons.Count; i++)
            {
                for (int j = 1; j <= NumberOfLevelsInSeasons[i-1]; j ++)
                {
                    string filename = string.Format("level_{0}_{1}_{2}", i.ToString(), j.ToString(), "s");
                    LevelStatusSaveState state = new LevelStatusSaveState
                    {
                        LevelName =  filename,
                        IsLocked = false,
                        IndexOfCoinsCollected = new List<int>(),
                        IndexOfDiamondsCollected = new List<int>(),
                        BestTime = 0,
                        BestTries = 0,
                    };
                    Debug.Log("saving status of level");
                    state.Save();
                    Debug.Log("saved status");
                    
                }
            }
        }

        if (PlayerPrefs.GetInt("levelIndex") == 0)
        {
            PlayerPrefs.SetInt("levelIndex", 1);
        }
                
    }
    
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
