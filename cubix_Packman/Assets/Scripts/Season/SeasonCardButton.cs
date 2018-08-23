using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonCardButton : MonoBehaviour {

	public int SeasonIndex;

	public void SeasonButtonOnClick()
	{
		SeasonManager.CurrentSeason = SeasonIndex;
		SeasonManager.SelectSeason();
	}	// Use this for initialization
}
