using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Boo.Lang;
using UnityEngine;

public class JigsawManager : MonoBehaviour
{

    public static int CurrentJigsawIndex;
    public static System.Collections.Generic.List<int> ListOfCurrentJigsawImgIndex = new System.Collections.Generic.List<int>();
    public static JigsawSeason ListOfJigsawSeasons = new JigsawSeason();

    private void Awake()
    {
        Load();
    }

    [Serializable]
    public class JigsawSeason
    {
        public System.Collections.Generic.List<Season> JigsawSeasonList;
    }
    
    [Serializable]
    public struct Season
    {
        public string Name;
        public System.Collections.Generic.List<int> ListOfJigsaws;
        public System.Collections.Generic.List<string> ListOfLevels;
    }
    
    public static void Load()
    {
        var directoryAll = Application.streamingAssetsPath +  "/Jigsaw/JigsawList";

        string jsonStringAll;
        
        if(Application.platform == RuntimePlatform.Android)
        {
            var readerAll = new WWW(directoryAll);
            while (!readerAll.isDone) { }
            
            var readerGot = new WWW(directoryAll);
            while (!readerGot.isDone) { }
            
            jsonStringAll = readerAll.text;
        }
        else
        {
            jsonStringAll = File.ReadAllText(directoryAll);
        }
        ListOfJigsawSeasons = JsonUtility.FromJson<JigsawSeason>(jsonStringAll);
    }

    public static void Save()
    {
        var directoryAll = Application.streamingAssetsPath + "/Jigsaw/JigsawList";

        var jsonStringAll = JsonUtility.ToJson(ListOfJigsawSeasons);
        
        File.WriteAllText(directoryAll, jsonStringAll);
    }

    public static System.Collections.Generic.List<int> CurrentJigsawImgList()
    {
        System.Collections.Generic.List<int> listOfImgsIndexs = new System.Collections.Generic.List<int>();
        for (int i = 4 * CurrentJigsawIndex - 3; i <= 4 * CurrentJigsawIndex; i++)
        {
            listOfImgsIndexs.Add(i);
        }

        return listOfImgsIndexs;
    }

    public static System.Collections.Generic.List<int> ListOfPicesGotInCurrentJigsaw()
    {
        TreasureManager.Load();
        System.Collections.Generic.List <int> listOfIndexsInCurrentJigsaw = CurrentJigsawImgList();
        System.Collections.Generic.List<int> listOfPices = new System.Collections.Generic.List<int>();
        
        foreach (var chest in TreasureManager.TreasureListGot.Treasures)
        {
            foreach (var i in listOfIndexsInCurrentJigsaw)
            {
                if(chest.ChestIndex == i)
                    listOfPices.Add(chest.ChestIndex);
            }
        }

        return listOfPices;
    }
}
