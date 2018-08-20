using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathPathTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Goliath.MovementFlag = true;
            Goliath.Destination = transform.parent.transform.localPosition;
        }
    }
}
