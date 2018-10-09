using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFileEditor : MonoBehaviour
{


    public InputField LevelName;
    public InputField Locked;
    public InputField MaxTime;
    public InputField BestTime;
    public InputField MaxTries;
    public InputField BestTries;
    public InputField Stars;
    public InputField Coins;
    public InputField Diamonds;

    public void Save()
    {
        SaveState levelState = new SaveState();
        LevelStatusSaveState levelStatusState = new LevelStatusSaveState();
        SaveManager sm = new SaveManager();

        levelStatusState.LevelName = LevelName.text;
        levelState = sm.Load(LevelName.text);
        levelState.MaxTime = int.Parse(MaxTime.text);
        levelState.MaxTries = int.Parse(MaxTries.text);
        
        levelState.Coins = int.Parse(Coins.text);
        levelState.Diamonds = int.Parse(Diamonds.text);

        levelStatusState.Stars = int.Parse(Stars.text);
        levelStatusState.BestTime = int.Parse(BestTime.text);
        levelStatusState.BestTries = int.Parse(BestTries.text);

        if (Locked.text == "T" || Locked.text == "t")
        {
            levelStatusState.IsLocked = true;
        }
        else
        {
            levelStatusState.IsLocked = false;
        }
        levelStatusState.Save();
        sm.Save(levelState, LevelName.text);
        
        Load();
    }

    public void Load()
    {
        SaveState levelState = new SaveState();
        LevelStatusSaveState levelStatusState = new LevelStatusSaveState();
        SaveManager sm = new SaveManager();

        levelState = sm.Load(LevelName.text);
        levelStatusState.LevelName = LevelName.text;
        levelStatusState.Load();
        
        if (levelStatusState.IsLocked)
        {
            Locked.text = "T";
        }
        else
        {
            Locked.text = "F";
        }
        
        MaxTime.text = levelState.MaxTime.ToString();
        MaxTries.text = levelState.MaxTries.ToString();

        Coins.text = levelState.Coins.ToString();
        Diamonds.text = levelState.Diamonds.ToString();

        BestTime.text = levelStatusState.BestTime.ToString();
        BestTries.text = levelStatusState.BestTries.ToString();
        Stars.text = levelStatusState.Stars.ToString();
        
    }
}
