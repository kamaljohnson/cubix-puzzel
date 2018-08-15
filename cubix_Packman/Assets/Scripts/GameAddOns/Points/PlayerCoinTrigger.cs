﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinTrigger : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(gameObject.tag == "Point")
            {    
                AddOnManager.collectedPoint();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}