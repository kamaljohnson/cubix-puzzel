using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerActivateTrigger : MonoBehaviour
{

	public bool HammerActive;

	private void Start()
	{
		HammerActive = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && !HammerActive)
		{
			HammerActive = true;
		}
	}
}
