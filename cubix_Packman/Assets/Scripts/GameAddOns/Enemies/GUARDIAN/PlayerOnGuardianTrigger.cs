﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnGuardianTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.IsAlive = false;
        }
    }
}
