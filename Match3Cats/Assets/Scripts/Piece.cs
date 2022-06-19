using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [Header("Board Variables")]
    public bool isMatched = false;
    Vector2 firstTouchPosn;
    Vector2 finalTouchPosn;
    float swipeAngle;
    float swipeResist;
    Board board;
    public int pieceCol;
    public int pieceRow;
    FindMatches findMatches;
    

    [Header("Swipe Variables")]
    [SerializeField] float timeToSwap;
    [SerializeField] float moveTime;
    [SerializeField] float checkDelay;
    public int targetCol;
    public int targetRow;
    GameObject swapPiece;
    Vector2 tempPosn;
    int previousColumn;
    int previousRow;


    private void OnMouseDown()
    {
        //Debug.Log("In Piece:OnMouseDown");
        firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(firstTouchPosn);
    }

    private void OnMouseUp()
    {
        //Debug.Log("In Piece:OnMouseUp");
        finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(finalTouchPosn);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        Debug.Log("In Piece:CalculateAngle");
        swipeAngle = Mathf.Atan2(finalTouchPosn.y - firstTouchPosn.y, finalTouchPosn.x - firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
    }
}
