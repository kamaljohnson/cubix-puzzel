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
		Debug.Log("here");
		LevelName = levelname;
		LevelStatusSaveState state = new LevelStatusSaveState{
			LevelName	=  levelname,
		};
		/*state.Load();
		IsLocked = state.IsLocked;
		*/
		IsLocked = false;
		Status.text = IsLocked ? "LOCKED" : LevelName;
	}
}
