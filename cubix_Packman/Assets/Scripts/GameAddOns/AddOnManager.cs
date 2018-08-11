using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddOnManager : MonoBehaviour
{

    public static int NoOfPoints = 0;

    public static void addPoints()
    {
        NoOfPoints++;
    }

    public static void collectedPoint()
    {
        NoOfPoints--;
        Debug.Log("Points : " + NoOfPoints.ToString());
    }
}
