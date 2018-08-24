using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFileEditor : MonoBehaviour
{


    public InputField LevelName;
    public InputField Locked;
    public InputField MaxTries;

    public void Save()
    {
        Debug.Log("SAVE");
        SaveManager sm = new SaveManager();
        SaveState state = new SaveState();

        state = sm.Load(LevelName.text);
        if (Locked.text == "T" || Locked.text == "t" )
        {
            state.IsLocked = true;
        }
        else
        {
            state.IsLocked = false;
        }

        state.MaxTries = int.Parse(MaxTries.text);
        SaveManager.levelName = LevelName.text;
        sm.Save(state);
        Load();
    }

    public void Load()
    {
        Debug.Log("LOAD");
        SaveManager sm = new SaveManager();
        SaveState state = sm.Load(LevelName.text);

        Locked.text = state.IsLocked ? "T" : "F";
        
        MaxTries.text = state.MaxTries.ToString();
        
    }
}
