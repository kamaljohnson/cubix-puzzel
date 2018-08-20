using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathPathTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && Goliath.ResponseTimer >= Goliath.ResponseTime)
        {
            Goliath.ResponseTimer = 0;
            Goliath.MovementFlag = true;
            Goliath.TempDestination = transform.parent.transform.localPosition;
        }
    }
}
