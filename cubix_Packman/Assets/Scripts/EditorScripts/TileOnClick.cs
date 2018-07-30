using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileOnClick : MonoBehaviour {

    public bool clicked;
    public void OnMouseOver()
    {
        clicked = true;
    }
    public void Reset()
    {
        clicked = false;        
    }
}
