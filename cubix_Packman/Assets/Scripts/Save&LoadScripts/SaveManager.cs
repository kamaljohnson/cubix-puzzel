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
    public object JsonMapper { get; private set; }

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
        //string s = Helper.Serialize(state);
        //XmlSerializer serializer = new XmlSerializer(typeof(SaveState));
        //FileStream stream = new FileStream(Application.persistentDataPath +"/Levels/"+ levelName, FileMode.Create);
        //serializer.Serialize(stream, state);
        //Debug.Log(Application.persistentDataPath + "/Levels/" + levelName);
        //stream.Close();


        string Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
        //BinaryFormatter BF = new BinaryFormatter();
        
        string jsonString;

        jsonString = JsonUtility.ToJson(state);
        File.WriteAllText(Directory, jsonString);
    }
    public SaveState Load()
    {
        //XmlSerializer serializer = new XmlSerializer(typeof(SaveState));
        //FileStream stream = new FileStream(Application.persistentDataPath + "/Levels/" + levelName, FileMode.Open);
        //Debug.Log(Application.persistentDataPath + "/Levels/" + levelName);
        //state = (SaveState)serializer.Deserialize(stream);


        string Directory = Application.streamingAssetsPath + "/Levels/" + levelName;
        //BinaryFormatter BF = new BinaryFormatter();
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
        /*if (File.Exists(Directory))
        {

            Debug.Log("File Exits");

            WWW linkstream;

            linkstream = new WWW("file://" + Directory);



            while (!linkstream.isDone) { }

            MemoryStream MySaveFile = new MemoryStream(linkstream.bytes);

            state = (SaveState)BF.Deserialize(MySaveFile);
            MySaveFile.Close();

        }*/
        return state;
    }
}
