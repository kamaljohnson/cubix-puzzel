using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFileEditor : MonoBehaviour
{


    public InputField LevelName;
    public InputField Locked;
    public InputField MaxTries;
    public InputField BestTries;
    public InputField Stars;
    public InputField Coins;
    public InputField Diamonds;

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
        state.BestTries = int.Parse(BestTries.text);
        state.Stars = int.Parse(Stars.text);
        if (int.Parse(Coins.text) == 0)
        {
            state.IndexOfCoinsCollected = new List<int>();
        }
        if (int.Parse(Diamonds.text) == 0)
        {
            state.IndexOfDiamondsCollectd = new List<int>();
        }
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
        BestTries.text = state.BestTries.ToString();
        Stars.text = state.Stars.ToString();
        Coins.text = state.IndexOfCoinsCollected.Count.ToString();
        Diamonds.text = state.IndexOfDiamondsCollectd.Count.ToString();
    }
}
