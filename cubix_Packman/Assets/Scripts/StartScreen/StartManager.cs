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
        
        for (int i = 1; i <= NumberOfLevelsInSeasons.Count; i++)
        {
            for (int j = 1; j <= NumberOfLevelsInSeasons[i-1]; j ++)
            {
                string filename = string.Format("level_{0}_{1}_{2}", i.ToString(), j.ToString(), "s");
                
                string directory;
                if (filename.Split('_')[3] == "s")
                {
                    directory = Application.persistentDataPath + "/" + filename;
                }
                else
                {
                    directory = Application.streamingAssetsPath + "/" + filename;
                }
                
                if (!File.Exists(directory))
                {
                    Debug.Log("creating file");
                    LevelStatusSaveState state = new LevelStatusSaveState
                    {
                        LevelName = filename ,
                        IsLocked = false,
                        IndexOfCoinsCollected = new List<int>(),
                        IndexOfDiamondsCollected = new List<int>(),
                        BestTime = 0,
                        BestTries = 0,
                    };
                
                    var file = File.CreateText(directory);
                    var jsonString = JsonUtility.ToJson(state);
                    file.WriteLine(jsonString);
                    file.Close();
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
