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

    
}
