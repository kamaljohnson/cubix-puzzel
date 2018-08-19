using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerActivateTrigger : MonoBehaviour
{

	public bool hammerActive;

	private void Start()
	{
		hammerActive = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !hammerActive)
		{
			hammerActive = true;
		}
	}
}
