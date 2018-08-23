using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class SeasonManager : MonoBehaviour
{
    private static bool _created = false;

    public int NumberOfSeasons = 5;
    public static int CurrentSeason;
    public static int NumberOfLevelsInSeason;

    public GameObject SeasonCardPrefab;
    public Transform SeasonCardParentTrasform;
    List<GameObject> AllSeasonCards = new List<GameObject>();

    void Awake()
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
        var levelName = string.Format("level_{0}_", CurrentSeason.ToString());
        NumberOfLevelsInSeason = 0;

        while (true)
        {
            levelName += i.ToString();
            Debug.Log(levelName);
            var directory = string.Format("{0}/Levels/{1}", Application.streamingAssetsPath, levelName);
            i++;
            if (System.IO.File.Exists(directory))
            {
                NumberOfLevelsInSeason++;
            }
            else
            {
                break;
            }

            levelName = string.Format("level_{0}_", CurrentSeason.ToString());
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
        
    public void InitiateSeasonCard(int cardIndex)
    {
        GameObject tempObj = Instantiate(SeasonCardPrefab, SeasonCardParentTrasform);
        tempObj.GetComponent<SeasonCardButton>().SeasonIndex = cardIndex;
        AllSeasonCards.Add(tempObj);
        
    }
}
