using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class LevelStatusSaveState
{
    public string LevelName;
    public bool IsLocked;
    public int Stars;
    public int BestTries;
    public int BestTime;
    public List<int> IndexOfCoinsCollected = new List<int>();
    public List<int> IndexOfDiamondsCollected = new List<int>();

    public void Load()
    {
        string directory;
        if (LevelName.Split('_')[3] == "s")
        {
            directory = Application.persistentDataPath + "/" + LevelName;
        }
        else
        {
            directory = Application.streamingAssetsPath + "/Levels/" + LevelName;
        }

        var jsonString = File.ReadAllText(directory);

        var ls = JsonUtility.FromJson<LevelStatusSaveState>(jsonString);

        IsLocked = ls.IsLocked;
        Stars = ls.Stars;
        BestTime = ls.BestTime;
        BestTries = ls.BestTries;
        IndexOfCoinsCollected = ls.IndexOfCoinsCollected;
        IndexOfDiamondsCollected = ls.IndexOfDiamondsCollected;
    }

    public void Save()
    {
     
        string directory;
        if (LevelName.Split('_')[3] == "s")
        {
            directory = Application.persistentDataPath + "/" + LevelName;
        }
        else
        {
            directory = Application.streamingAssetsPath + "/" + LevelName;
        }

        var jsonString = JsonUtility.ToJson(this);

        var fullPath = directory;
        try
        {
            if (!File.Exists(fullPath))
            {
                Debug.Log("creating file");
                LevelStatusSaveState state = new LevelStatusSaveState
                {
                    LevelName = LevelName ,
                    IsLocked = false,
                    IndexOfCoinsCollected = new List<int>(),
                    IndexOfDiamondsCollected = new List<int>(),
                    BestTime = 0,
                    BestTries = 0,
                };
                
                var file = File.CreateText(fullPath);
                jsonString = JsonUtility.ToJson(state);
                file.WriteLine(jsonString);
                file.Close();
            }
            else
            {
                Debug.Log("file exists in " + fullPath);
                File.WriteAllText(fullPath, jsonString);
            }
 
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }
}

[Serializable]
public class SaveState {
    
    public float LevelSize;
    
    // 3 Starts factors 
    public int MaxTries;
    public int MaxTime;
    public int Coins;

    public int Diamonds;
    
    public List<SaveavleNode> Node;
    
}

public class Node
{
    public PrefabType Type;
    public Transform transform;
}

[Serializable]
public class SaveavleNode
{
    //ID
    public PrefabType T;

    //transform.position
    public float px;
    public float py;
    public float pz;

    //transform.rotation
    public int rx;
    public int ry;
    public int rz;

    public void ConvertToSaveable( PrefabType type, Transform transform)
    {
        this.T = type;

        float tempPosx = transform.position.x;
        float tempPosy = transform.position.y;
        float tempPosz = transform.position.z;

        
        float tempRotx = transform.eulerAngles.x;
        float tempRoty = transform.eulerAngles.y;
        float tempRotz = transform.eulerAngles.z;

        this.px  = (float)(Math.Round((double)tempPosx, 1));
        this.py  = (float)(Math.Round((double)tempPosy, 1));
        this.pz  = (float)(Math.Round((double)tempPosz, 1));

        this.rx  = (int)(Math.Round((double)tempRotx, 0));
        this.ry  = (int)(Math.Round((double)tempRoty, 0));
        this.rz  = (int)(Math.Round((double)tempRotz, 0));
        
    }
    public Node ConvertToNode()
    {
        Node node = new Node();

        node.Type = this.T;

        GameObject tempObj = new GameObject();
        node.transform = tempObj.transform;

        node.transform.position = new Vector3(this.px, this.py, this.pz);

        Quaternion rotation = Quaternion.Euler(this.rx, this.ry, this.rz);
        node.transform.rotation = rotation;
        return node;
    }
}