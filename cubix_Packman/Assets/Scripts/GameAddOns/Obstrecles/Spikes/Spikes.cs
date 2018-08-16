using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public GameObject Spike;
    private float _timer;
    private int _currntIndex;
    public int LocalTimerIndex;
    public int IndexLimit;
    
    private void Update()
    {
        _timer += Time.deltaTime;

        _currntIndex = (int)_timer;
        
        if (_currntIndex == LocalTimerIndex)
        {
            SpikeAction();
        }
        else
        {
            SpikeAction();
        }

        if (_currntIndex > IndexLimit)
        {
            _timer = 0;
        }
    }

    private void SpikeAction()
    {
        Spike.SetActive(!Spike.activeSelf);
    }
    public void SetLeader()
    {
        LocalTimerIndex = 0;
    }
}
