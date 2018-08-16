using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static bool IsAlive = true;
    
    public void Update()
    {
        if (!IsAlive)
        {
            Debug.Log("activating the checkpoint");
            GameManager.IsPlaying = false;
            CheckPoint.ActivateCheckPoint();
        }
    }
}
