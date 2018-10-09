using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{

    public static SaveManager Instance { set; get; }

    public SaveState state;

    public static string levelName;
    public static float levelSize;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void Save(SaveState state)
    {

        Debug.Log("saving level 2 " + levelName);
        string Directory;
        if (levelName.Split('_')[3] == "s")
        {
            Directory = Application.persistentDataPath + "/" + levelName;
        }
        else
        {
            Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
        }
        
        string jsonString;

        jsonString = JsonUtility.ToJson(state);
        File.WriteAllText(Directory, jsonString);    
    }
    public void Save(SaveState state, string LevelName)
    {
        Debug.Log("saving level 1");
        string Directory;
        if (levelName.Split('_')[3] == "s")
        {
            Directory = Application.persistentDataPath + "/" + levelName;
        }
        else
        {
            Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
        }     
        string jsonString;

        jsonString = JsonUtility.ToJson(state);
        File.WriteAllText(Directory, jsonString); 
    }
    public SaveState Load()
    {
        Debug.Log("loading level 1" + levelName);
        string Directory;
        if (levelName.Split('_')[3] == "s")
        {
            Directory = Application.persistentDataPath + "/" + levelName;
        }
        else
        {
            Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
        }
        string jsonString;
        if(Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(Directory);
            while (!reader.isDone) { }

            jsonString = reader.text;
        }
        else
        {
            jsonString = File.ReadAllText(Directory);
        }
        state = JsonUtility.FromJson<SaveState>(jsonString);
        return state;
    }
    public SaveState Load(string LevelName)
    {
        
        Debug.Log("loading level 2");
        string Directory;
        if (levelName.Split('_')[3] == "s")
        {
            Directory = Application.persistentDataPath + "/" + levelName;
        }
        else
        {
            Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
        } 
        string jsonString;
        if(Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(Directory);
            while (!reader.isDone) { }

            jsonString = reader.text;
        }
        else
        {
            jsonString = File.ReadAllText(Directory);
        }
        state = JsonUtility.FromJson<SaveState>(jsonString);
        return state;
    }
}
