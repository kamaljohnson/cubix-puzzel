using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathAttackSwitchPlayerTrigger : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GoliathAttackSwitch.NextSwitch();
            Goliath.Atacked();
        }
    }
    
}
