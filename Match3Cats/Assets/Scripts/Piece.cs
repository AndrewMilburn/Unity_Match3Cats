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

    private void OnMouseUp() {
        finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(finalTouchPosn);
        CalculateAngle();
    }

    private void CalculateAngle() {
        swipeAngle = Mathf.Atan2(finalTouchPosn.y - firstTouchPosn.y, finalTouchPosn.x - firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
    }
}
