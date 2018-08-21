using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathPathTrigger : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Goliath.Destination = transform.parent.transform.localPosition;
        }
    }
}
