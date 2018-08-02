using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static string GetCurrentLevel()
    {
        return (PlayerPrefs.GetString("current_level"));
    }

    public static void NextLevel()
    {
        string current_level = GetCurrentLevel();
        string[] intermediate_level_string_list = current_level.Split('_');
        int level = Int32.Parse(intermediate_level_string_list[1]);
        level++;
        intermediate_level_string_list[1] = level.ToString();
        current_level = intermediate_level_string_list[0] + "_" + intermediate_level_string_list[1];
        PlayerPrefs.SetString("current_level", current_level);
    }
}
