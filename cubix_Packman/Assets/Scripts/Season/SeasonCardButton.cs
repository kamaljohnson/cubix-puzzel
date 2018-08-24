using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeasonCardButton : MonoBehaviour {

	public int SeasonIndex;
	public int NumberOfLevels;
	public int NumberOfJigsaws;
	public int JigsawsSolved;
	public int CoinsCollected;
	public int DiamondsCollected;

	public Text NumberOfLevelsText;
	public Text SeasonNameText;
	
	public void SeasonButtonOnClick()
	{
		SeasonManager.CurrentSeason = SeasonIndex;
		SeasonManager.SelectSeason();
	}	// Use this for initialization

	public void SetCardDetails(int cardIndex)
	{
		SeasonIndex = cardIndex;
		NumberOfLevels = SeasonManager.NumberOfLevelsInSeasons[cardIndex - 1];
		NumberOfLevelsText.text = string.Format("{0} LEVELS", NumberOfLevels.ToString());
		SeasonNameText.text = string.Format("SEASON {0}", SeasonIndex.ToString());
	}
}
