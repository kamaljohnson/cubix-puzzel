using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AddOnManager : MonoBehaviour
{

    public static int NoOfPoints = 0;
    
    public static void addPoints()
    {
        NoOfPoints++;
    }

    public static void collectedPoint()
    {
        playerController.PointsCollected++;
        Debug.Log("Points : " + NoOfPoints.ToString());
    }
}
