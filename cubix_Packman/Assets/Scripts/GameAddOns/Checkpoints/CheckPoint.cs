using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{

    public static Transform MazeCurrentCheckPointTransform;
    public static Transform PlayerCurrentCheckPointTransform;
    
    public Transform MazeCheckPointTransition;
    public Transform PlayerCheckPointTransition;


    public void SetCheckPointTransfrom()
    {
        Debug.Log("checkpoint set");
        MazeCurrentCheckPointTransform = MazeCheckPointTransition;
        PlayerCurrentCheckPointTransform = PlayerCheckPointTransition;
    }

    public void SetLocalValues(Transform mazeTransform, Transform plaerTransform)
    {
        MazeCheckPointTransition = mazeTransform;
        PlayerCheckPointTransition = plaerTransform;
    }
}
