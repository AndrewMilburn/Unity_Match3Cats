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
    float swipeResist = 0.4f;
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

    private void Start()
    {
        Debug.Log("In Piece:Start");
        board = FindObjectOfType<Board>();
        targetCol = (int)transform.position.x;
        targetRow = (int)transform.position.y;
        pieceCol = targetCol;
        pieceRow = targetRow;
    }

    private void Update()
    {
        Debug.Log("In Piece:Update");
        if (Mathf.Abs(targetCol - transform.position.x) > 0.01f)
        {
            tempPosn = new Vector2(Mathf.Lerp((float)pieceCol, (float)targetCol, moveTime / timeToSwap), pieceRow);
            transform.position = tempPosn;
            moveTime += Time.deltaTime;
        }
        else
        {
            tempPosn = new Vector2(targetCol, targetRow);
            transform.position = tempPosn;
            pieceCol = targetCol;
            board.pieceArray[pieceCol, pieceRow] = this.gameObject;
            moveTime = 0f;
        }
    }

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
        if (Vector2.Distance(firstTouchPosn, finalTouchPosn) > swipeResist)
        {
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        Debug.Log("In Piece:CalculateAngle");
        swipeAngle = Mathf.Atan2(finalTouchPosn.y - firstTouchPosn.y, finalTouchPosn.x - firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces()
    {
        Debug.Log("In Piece:MovePieces");
        if (swipeAngle > -45 && swipeAngle <= 45 && pieceCol < board.boardWidth)    // Right Swipe
        {
            Debug.Log("Swipe Right");
            swapPiece = board.pieceArray[pieceCol + 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            targetCol = pieceCol + 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && pieceRow < board.boardHeight)    // Up Swipe
        {
            Debug.Log("Swipe Up");
            swapPiece = board.pieceArray[pieceCol, pieceRow + 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            targetRow = pieceRow + 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && pieceCol > 0)    // Left Swipe
        {
            Debug.Log("Swipe Left");
            swapPiece = board.pieceArray[pieceCol - 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            targetCol = pieceCol - 1;
        }
        else if (swipeAngle <= -45 && swipeAngle > -135 && pieceRow > 0)    // Down Swipe
        {
            Debug.Log("Swipe Down");
            swapPiece = board.pieceArray[pieceCol, pieceRow - 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            targetRow = pieceRow - 1;
        }
    }
}
