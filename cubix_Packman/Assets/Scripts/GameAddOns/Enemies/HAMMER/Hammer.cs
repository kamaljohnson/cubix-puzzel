using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject hammer;
    public GameObject trigger;
    private void Update()
    {
        if (trigger.GetComponent<hammerActivateTrigger>().hammerActive)
        {
            hammer.GetComponent<Animation>().Play("Hammer");
            trigger.GetComponent<hammerActivateTrigger>().hammerActive = false;
        }
    }
}
