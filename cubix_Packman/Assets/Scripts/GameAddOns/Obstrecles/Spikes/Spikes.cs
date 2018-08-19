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
        
        _timer += Time.deltaTime * 3;
        
        _currntIndex = (int)_timer;
                
        if (_currntIndex == LocalTimerIndex && _spikeFlag) 
        {
            _spikeFlag = false;
            SpikeAction();
        }
        else
        {
            _spikeFlag = true;
        }
        if (_currntIndex == IndexLimit-1)
        {
            _timer = 0;
        }
    }

    private void SpikeAction()
    {
        if(EditorMode)
            return;
        Spike.GetComponent<Animation>().Play("Spike");
    }
    
}
