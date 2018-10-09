using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class GoliathAttackSwitch : MonoBehaviour {

    public static List<GameObject> SwitchLists = new List<GameObject>();

    public static int currentIndex;

    private void Start()
    {
        if(Goliath.EditorMode)
            return;
        foreach (var s in SwitchLists)
        {
            s.SetActive(false);
        }

        currentIndex = 0;
        SwitchLists[currentIndex].SetActive(true);
    }

    public static void NextSwitch()
    {
        SwitchLists[currentIndex].SetActive(false);
        if(Goliath.health == 0)
            return;
        
        currentIndex++;
        if (currentIndex == SwitchLists.Count)
            currentIndex = 0;
        
        SwitchLists[currentIndex].SetActive(true);
    }
}
