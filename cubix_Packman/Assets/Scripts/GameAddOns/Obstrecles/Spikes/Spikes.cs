using UnityEngine;

public class Spikes : MonoBehaviour
{
    public static bool EditorMode;
    public static int CurrentFlagIndex;
    public static bool SpikeInitializeFlag;
    public GameObject Spike;
    private float _timer;
    private int _currntIndex;
    public int LocalTimerIndex;
    public int IndexLimit;

    private bool _spikeFlag = false;
    
    private void Update()
    {
        if(EditorMode)
            return;
        
        _timer += Time.deltaTime * 2;
        
        _currntIndex = (int)_timer;
                
        if (_currntIndex == LocalTimerIndex)
        {
            SpikeAction(true);
        }
        else
        {
            SpikeAction(false);
        }

        if (_currntIndex == IndexLimit-1)
        {
            _timer = 0;
        }
    }

    private void SpikeAction(bool activity)
    {
        Spike.SetActive(activity);
    }
    
}
