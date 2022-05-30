using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    Vector2 firstTouchPosn;
    Vector2 finalTouchPosn;
    float swipeAngle = 0;


    private void OnMouseDown() {
        firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(firstTouchPosn);
    }
}
