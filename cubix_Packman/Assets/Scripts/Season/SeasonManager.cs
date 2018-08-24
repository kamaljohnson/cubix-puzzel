using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class SeasonManager : MonoBehaviour
{
    private static bool _created = false;

    public int NumberOfSeasons = 5;
    public static int CurrentSeason;
    public List<int> NumberOfLevelsInSeason;
    public static List<int> NumberOfLevelsInSeasons;
    public static int NumberOfLevelsInCurrentSeason;

    public GameObject SeasonCardPrefab;
    public Transform SeasonCardParentTrasform;
    List<GameObject> AllSeasonCards = new List<GameObject>();

    void Awake()
    {
        NumberOfLevelsInSeasons = NumberOfLevelsInSeason;

        if (!_created)
        {
            DontDestroyOnLoad(this.gameObject);
            _created = true;
        }
        for(var season = 1; season <= NumberOfSeasons; season++)
            InitiateSeasonCard(season);
    }

    public static void SelectSeason()
    {

        var i = 1;
        var levelName = string.Format("level_{0}_", CurrentSeason.ToString());
        NumberOfLevelsInCurrentSeason = 0;

        if (Application.platform == RuntimePlatform.Android)
        {
            NumberOfLevelsInCurrentSeason = NumberOfLevelsInSeasons[CurrentSeason-1];
        }
        else
        {


            while (true)
            {
                levelName += i.ToString();
                i++;
                var levelfile = string.Format("{0}/Levels/{1}", Application.streamingAssetsPath, levelName);

                FileInfo fileInfo = new FileInfo(levelfile);

                if (fileInfo.Exists == true)
                {
                    NumberOfLevelsInCurrentSeason++;
                }
                else
                {
                    break;
                }

                levelName = string.Format("level_{0}_", CurrentSeason.ToString());
            }
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
        
    public void InitiateSeasonCard(int cardIndex)
    {
        GameObject tempObj = Instantiate(SeasonCardPrefab, SeasonCardParentTrasform);
        tempObj.GetComponent <SeasonCardButton>().SetCardDetails(cardIndex);
        AllSeasonCards.Add(tempObj);
        
    }
}
