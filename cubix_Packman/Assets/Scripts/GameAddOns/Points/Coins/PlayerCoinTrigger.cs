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
                GameManager.IndexOfCoinsCollected.Add(transform.parent.gameObject.GetComponent<Points>().Index);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
