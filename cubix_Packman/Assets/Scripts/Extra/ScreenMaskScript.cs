using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenMaskScript : MonoBehaviour
{

	public static bool Trigger;
	Animator _anim;
	
	private void Start ()
	{
		_anim = GetComponent<Animator>();
	}

	private void Update () {

		if (Trigger)
		{
			MakeTransition();
		}
	}

	private void MakeTransition()
	{
		Trigger = false;
		_anim.Play("TransitionAnimation", -1, 0f);
	}
}
