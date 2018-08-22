using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TreasureManager : MonoBehaviour
{

    public static TreasureChest TreasureListAll;
    public static TreasureChest TreasureListGot;
    
    [Serializable]
    public class TreasureChest
    {
        public List<Chest> AllTreasureChests;
    }

    public struct Chest
    {
        public String LevelName;
        public String PuzzleName;
        public int PuzzleIndex;
    }
    
    
    public static void Save()
    {

        var directoryAll = Application.streamingAssetsPath + "/Treasures/TreasureList";
        var directoryGot = Application.streamingAssetsPath + "/Treasures/TreasureList";

        var jsonStringAll = JsonUtility.ToJson(TreasureListAll);
        var jsonStringGot = JsonUtility.ToJson(TreasureListGot);
        
        File.WriteAllText(directoryAll, jsonStringAll);
        File.WriteAllText(directoryAll, jsonStringGot);
    }
    public static void Load()
    {

        var directoryAll = Application.streamingAssetsPath + "/Treasures/TreasureList";
        var directoryGot = Application.streamingAssetsPath + "/Treasures/TreasureGot";

        string jsonStringAll;
        string jsonStringGot;
        
        if(Application.platform == RuntimePlatform.Android)
        {
            var readerAll = new WWW(directoryAll);
            while (!readerAll.isDone) { }
            
            var readerGot = new WWW(directoryAll);
            while (!readerGot.isDone) { }
            
            jsonStringAll = readerAll.text;
            jsonStringGot = readerGot.text;
        }
        else
        {
            jsonStringAll = File.ReadAllText(directoryAll);
            jsonStringGot = File.ReadAllText(directoryGot);
        }
        TreasureListAll = JsonUtility.FromJson<TreasureChest>(jsonStringAll);
        TreasureListAll = JsonUtility.FromJson<TreasureChest>(jsonStringGot);
    }
}
