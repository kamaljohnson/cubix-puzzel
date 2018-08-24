using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondOnPlayerTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Diamond"))
            {
                AddOnManager.collectedPoint();
                GameManager.IndexOfDiamondsCollected.Add(transform.parent.gameObject.GetComponent<Diamond>().Index);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
