using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOnPlayerTrigger : MonoBehaviour {
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("here");
            TreasureManager.AddChestToGotTreasures(Chest.CurrentChest);
            transform.parent.gameObject.SetActive(false);
        }
    }
}
