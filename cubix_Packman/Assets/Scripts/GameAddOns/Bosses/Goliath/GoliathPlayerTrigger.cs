using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoliathPlayerTrigger : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Player.IsAlive = false;
            Goliath.Destination = Goliath.InitialPosition;
        }
    }
}