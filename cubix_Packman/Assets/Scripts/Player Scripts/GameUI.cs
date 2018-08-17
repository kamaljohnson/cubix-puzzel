using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Text Points;
    public Text Keys;
    public Text Moves;
    
    private void Update()
    {
        Points.text = playerController.PointsCollected.ToString();
        Moves.text = Player.NoOfMoves.ToString();
    }
}
