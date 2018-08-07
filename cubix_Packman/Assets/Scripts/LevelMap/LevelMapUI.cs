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
        Debug.Log("called the funtion");
        int i = 0;
        string levelName = "level_";
        string Directory;
        //BinaryFormatter BF = new BinaryFormatter();
        string jsonString;
        while (true)
        {
            levelName += i.ToString();
            Debug.Log("level : " + levelName);
            Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
            i++;
            if (System.IO.File.Exists(Directory))
            {
                InitiateLevelCard(levelName);
            }
            else
            {
                return;
            }
            levelName = levelName.Split('_')[0] + "_";
        }
    }

    public void InitiateLevelCard(string cardName)
    {
        Debug.Log("instantiated the card");
        GameObject tempObj = Instantiate(LevelCardPrefab, LevelCardParentTrasform);
        tempObj.GetComponent<LevelCardButton>().LevelName = cardName;
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
