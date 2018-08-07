using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCardButton : MonoBehaviour {

	// Use this for initialization
	public string LevelName;
	void Start () {
		
	}

	public void OnCardClick()
	{
		LevelManager.CurrentLevel = LevelName;
		LevelManager.ActivateLevel();
	}
}
