using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static bool IsAlive = true;
    public static int NoOfMoves = 0;
    private bool deathFlag = false;
    public void Update()
    {
        if (!IsAlive && !deathFlag)
        {
            deathFlag = true;
            NoOfMoves++;
            Debug.Log("activating the checkpoint");
            GameManager.IsPlaying = false;
            CheckPoint.ActivateCheckPoint();
        }

        if (IsAlive)
        {
            deathFlag = false;
            GameManager.IsPlaying = true;
        }
    }
}
