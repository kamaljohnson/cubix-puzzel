using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardButton : MonoBehaviour {

	// Use this for initialization
	public string LevelName;
	public bool IsLocked;
	public Text Status;
	void Start () {
		
	}

	public void OnCardClick()
	{		
		if (!IsLocked)
		{
			LevelManager.CurrentLevel = LevelName;
			LevelManager.ActivateLevel();
		}
	}

	public void SetDetails(string levelname)
	{
		LevelName = levelname;
		/*
		LevelStatusSaveState state = new LevelStatusSaveState{
			LevelName	=  levelname,
		};
		state.Load();
		IsLocked = state.IsLocked;
		*/

		var levelIndex = 0;
		var flag = false;
		for (var i = 1; i <= SeasonManager.NumberOfLevelsInSeasons.Count; i++)
		{
			for (var j = 1; j <= SeasonManager.NumberOfLevelsInSeasons[i - 1]; j++)
			{
				Debug.Log("*" + i.ToString());
				
				levelIndex += 1;
				if (LevelName == string.Format("level_{0}_{1}_{2}", i.ToString(), j.ToString(), "L"))
				{
					flag = true;
					break;
				}
			}

			if (flag)
				break;
		}
		if(levelIndex > PlayerPrefs.GetInt("levelIndex"))
			IsLocked = true;
		else
		{
			IsLocked = false;
		}
		Status.text = IsLocked ? "LOCKED" : LevelName;
	}
}
