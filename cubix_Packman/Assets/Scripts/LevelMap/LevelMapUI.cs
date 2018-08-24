using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelMapUI : MonoBehaviour
{
    public GameObject LevelCardPrefab;
    public Transform LevelCardParentTrasform;
    public List<GameObject> AllLevelCards = new List<GameObject>();
    private void Start()
    {
        LoadMap();        
    }

    public void LoadMap()
    {
        string levelName = string.Format("level_{0}_", SeasonManager.CurrentSeason.ToString());

        for (int i = 1; i <= SeasonManager.NumberOfLevelsInCurrentSeason; i++)
        {
            InitiateLevelCard(string.Format("level_{0}_{1}", SeasonManager.CurrentSeason.ToString(), i.ToString()));
        }
    }

    public void InitiateLevelCard(string cardName)
    {
        GameObject tempObj = Instantiate(LevelCardPrefab, LevelCardParentTrasform);
        tempObj.GetComponent<LevelCardButton>().SetDetails(cardName);
        AllLevelCards.Add(tempObj);
        
    }

    public void DeleteAllCards()
    {
        for (int i = 0; i < AllLevelCards.Count; i++)
        {
            Destroy(AllLevelCards[i]);
        }
        AllLevelCards = new List<GameObject>();
    }
}
