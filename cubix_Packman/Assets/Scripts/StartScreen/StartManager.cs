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

        Debug.Log(Application.persistentDataPath);
        
        PlayerPrefs.SetString("gameStatus", "NotSet");
        if (PlayerPrefs.GetString("gameStatus") == "NotSet")
        {
            Debug.Log("Settings things up. . .");
            PlayerPrefs.SetString("gameStaus", "Set");
            // set all the level status to initial values
            for (int i = 1; i <= NumberOfLevelsInSeasons.Count; i++)
            {
                for (int j = 1; j <= NumberOfLevelsInSeasons[i-1]; j ++)
                {
                    string filename = string.Format("level_{0}_{1}", i.ToString(), j.ToString());
                    LevelStatusSaveState state = new LevelStatusSaveState
                    {
                        LevelName =  filename,
                        IsLocked = false,
                        IndexOfCoinsCollected = new List<int>(),
                        IndexOfDiamondsCollected = new List<int>(),
                        BestTime = 0,
                        BestTries = 0,
                    };
                    Debug.Log(filename);
                    state.Save();
                }
            }
        }
    }
    
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
