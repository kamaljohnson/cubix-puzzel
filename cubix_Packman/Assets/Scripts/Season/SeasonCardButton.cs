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
	public Text CoinsCollectedText;
	public Text DiamondsCollectedText;
	
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
				
		for (int i = 1; i <= NumberOfLevels; i++)
		{
			LevelStatusSaveState state = new LevelStatusSaveState{
				LevelName = string.Format("level_{0}_{1}_{2}", cardIndex.ToString(), i.ToString(), "L")
			};
			Debug.Log(state.LevelName);

/*
			state.Load();

			DiamondsCollected += state.IndexOfDiamondsCollected.Count;
			CoinsCollected += state.IndexOfCoinsCollected.Count;
*/
			CoinsCollected = 0;
			DiamondsCollected = 0;
			
		}

		CoinsCollectedText.text = string.Format("{0} COINS", CoinsCollected.ToString());
		DiamondsCollectedText.text = string.Format("{0} DIAMONDS", DiamondsCollected.ToString());
	}
}
