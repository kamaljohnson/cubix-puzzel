using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static string CurrentLevel;
    public static void ActivateLevel()
    {
        GameManager.CurrentLevel = CurrentLevel;
        Debug.Log("the level is set to " + CurrentLevel);
        PlayerPrefs.SetString("current_level", CurrentLevel);
        GameManager.LoadGameScene();
    }
    public static string GetCurrentLevel()
    {
        return (PlayerPrefs.GetString("current_level"));
    }

    public static void NextLevel()
    {
        string current_level = GetCurrentLevel();
        string[] intermediate_level_string_list = current_level.Split('_');
        /*int theam = Int32.Parse(intermediate_level_string_list[1]);*/
        int level = Int32.Parse(intermediate_level_string_list[1]);
        level++;
        intermediate_level_string_list[1] = level.ToString();
        current_level = string.Format("{0}_{1}", intermediate_level_string_list[0]/*, intermediate_level_string_list[1]*/, intermediate_level_string_list[1]);
        PlayerPrefs.SetString("current_level", current_level);
    }    //TODO create script to change the current level sected form the level map
    
}
