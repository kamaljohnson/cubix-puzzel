using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoinTrigger : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(gameObject.CompareTag("Point"))
            {    
                AddOnManager.collectedPoint();
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
