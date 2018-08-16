using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static bool IsAlive;

    public void Update()
    {
        if (!IsAlive)
        {
            Debug.Log("activating the checkpoint");
            CheckPoint.ActivateCheckPoint();
        }
    }
}
