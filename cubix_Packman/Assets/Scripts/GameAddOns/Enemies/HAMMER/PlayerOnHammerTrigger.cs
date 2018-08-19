using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnHammerTrigger : MonoBehaviour {

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			Player.IsAlive = false;
		}
	}
}
