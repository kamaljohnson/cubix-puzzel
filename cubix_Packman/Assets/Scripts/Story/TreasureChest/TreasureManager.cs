using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class TreasureManager : MonoBehaviour
{
    
    public static TreasureChest TreasureListAll;
    public static TreasureChest TreasureListGot;

    void Awake()
    {
        Load();
    }
    
    [Serializable]
    public class TreasureChest
    {
        public List<Chest> Treasures;

        public TreasureChest()
        {
            Treasures = new List<Chest>();    
        }
    }

    [Serializable]
    public struct Chest
    {
        public int ChestIndex;
        public String LevelName;
    }


    public static void AddChestToAllTreasures(Chest chest)
    {
        if (!TreasureListAll.Treasures.Contains(chest))
        {
            TreasureListAll.Treasures.Add(chest);
            Save();
            Debug.Log("added to all treasures. . .");
        }
    }

    public static void AddChestToGotTreasures(Chest chest)
    {
        if (TreasureListGot.Treasures.Count != 0)
        {
            if (!TreasureListGot.Treasures.Contains(chest))
            {
                TreasureListGot.Treasures.Add(chest);
                Save();
                Debug.Log("added to got treasures. . .");
            }
        }
        else
        {    
            TreasureListGot.Treasures.Add(chest);
            Save();
            Debug.Log("added to got treasures. . .");
        }
    }
    
    public static void Save()
    {

        var directoryAll = Application.streamingAssetsPath + "/Treasures/TreasureListAll";
        var directoryGot = Application.streamingAssetsPath + "/Treasures/TreasureListGot";

        var jsonStringAll = JsonUtility.ToJson(TreasureListAll);
        var jsonStringGot = JsonUtility.ToJson(TreasureListGot);
        
        File.WriteAllText(directoryAll, jsonStringAll);
        File.WriteAllText(directoryGot, jsonStringGot);
    }
    public static void Load()
    {

        var directoryAll = Application.streamingAssetsPath + "/Treasures/TreasureListAll";
        var directoryGot = Application.streamingAssetsPath + "/Treasures/TreasureListGot";

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
        TreasureListGot = JsonUtility.FromJson<TreasureChest>(jsonStringGot);
    }
}
