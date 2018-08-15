using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Key : MonoBehaviour
{

    public GameObject Lock;
    
    public void ActivateKey()
    {
        Debug.Log("I AM HERE");
        Lock.SetActive(!Lock.activeSelf);
    }

}
