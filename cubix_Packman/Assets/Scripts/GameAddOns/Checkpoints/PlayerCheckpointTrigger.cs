using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckpointTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            CheckPoint.SetCheckPointTransfrom(transform.parent.transform.localPosition);
        }
    }
}
