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

    public int NumberOfSeasons;
    public static int CurrentSeason;
    public static List<int> NumberOfLevelsInSeasons;
    public static int NumberOfLevelsInCurrentSeason;

    public GameObject SeasonCardPrefab;
    public Transform SeasonCardParentTrasform;
    List<GameObject> AllSeasonCards = new List<GameObject>();

    void Start()
    {

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
        
        NumberOfLevelsInCurrentSeason = NumberOfLevelsInSeasons[CurrentSeason-1];

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
        
    public void InitiateSeasonCard(int cardIndex)
    {
        GameObject tempObj = Instantiate(SeasonCardPrefab, SeasonCardParentTrasform);
        tempObj.GetComponent <SeasonCardButton>().SetCardDetails(cardIndex);
        AllSeasonCards.Add(tempObj);
        
    }

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
}
