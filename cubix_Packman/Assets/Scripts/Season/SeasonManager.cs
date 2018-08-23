using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SeasonManager : MonoBehaviour
{
    private static bool _created = false;

    public static int CurrentSeason;
    public static int NumberOfLevelsInSeason;

    void Awake()
    {
        CurrentSeason = 1;
        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
        SelectSeason();
    }

    public void SelectSeason()
    {
     
        var i = 1;
        var levelName = string.Format("level_{0}_", CurrentSeason.ToString());
        NumberOfLevelsInSeason = 0;
        
        while (true)
        {
            levelName += i.ToString();
            Debug.Log(levelName);
            var directory = string.Format("{0}/Levels/{1}", Application.streamingAssetsPath, levelName);
            i++;
            if (System.IO.File.Exists(directory))
            {
                NumberOfLevelsInSeason++;
            }
            else
            {
                break;
            }
            levelName = string.Format("level_{0}_", CurrentSeason.ToString());
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
