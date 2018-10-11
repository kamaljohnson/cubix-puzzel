using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static bool IsAlive = true;
    public static int NoOfMoves = 0;
    private bool _deathFlag = false;

    private float _respawnTimer = 0;
    private readonly float _respawnTime = 0.15f;
    public void Update()
    {
        if (!IsAlive && !_deathFlag)
        {   
            _deathFlag = true;
            NoOfMoves++;
            GameManager.IsPlaying = false;
            ScreenMaskScript.Trigger = true;
        }

        if (_deathFlag)
        {
            _respawnTimer += Time.deltaTime;
            Debug.Log(_respawnTimer.ToString() + " <---- ");
            if (_respawnTimer > _respawnTime)
            {
                _respawnTimer = 0;
                _deathFlag = false;
                IsAlive = true;
                
                CheckPoint.ActivateCheckPoint();
                Goliath.health = Goliath.MaxHealth;
                
                GameManager.IsPlaying = true;
            }
        }
    }
}
