using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{

    public int value;

    public void Start()
    {
        AddOnManager.addPoints();
    }

    private void Update()
    {
        
    }
}
