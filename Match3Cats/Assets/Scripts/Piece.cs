using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{ 
    Vector2 firstTouchPosn;
    Vector2 finalTouchPosn;
    float swipeAngle = 0;
    float swipeResist = 0.5f;
    Board board;
    [SerializeField]int pieceCol;
    [SerializeField]int pieceRow;
    GameObject swapPiece;
    [SerializeField]int targetCol;
    [SerializeField]int targetRow;
    Vector2 tempPosn;
    [SerializeField] float timeToSwap = 0.5f;
    [SerializeField] float moveTime;
    FindMatches findMatches;
    public bool isMatched = false;


    private void Start()
    {
        board = FindObjectOfType<Board>();
        targetCol = (int)transform.position.x;
        targetRow = (int)transform.position.y;
        pieceCol = targetCol;
        pieceRow = targetRow;
        findMatches = FindObjectOfType<FindMatches>();
    }

    private void Update()
    {
        findMatches.FindAllMatches();
        if(isMatched)
        {
            SpriteRenderer pieceSprite = GetComponent<SpriteRenderer>();
            pieceSprite.color = new Color(1f, 1f, 1f, 1f);
        }

        if(Mathf.Abs(targetCol - transform.position.x) > 0.1f)
        {
            tempPosn = new Vector2(Mathf.Lerp((float)pieceCol, (float)targetCol, moveTime / timeToSwap), pieceRow);
            transform.position = tempPosn;
            moveTime += Time.deltaTime;
        }
        else if(targetCol != pieceCol)
        {
            tempPosn = new Vector2(targetCol, targetRow);
            transform.position = tempPosn;
            pieceCol = targetCol;
            board.pieceArray[pieceCol, pieceRow] = this.gameObject;
        }
        if (Mathf.Abs(targetRow - transform.position.y) > 0.1f)
        {
            tempPosn = new Vector2(pieceCol, Mathf.Lerp((float)pieceRow, (float)targetRow, moveTime / timeToSwap));
            transform.position = tempPosn;
            moveTime += Time.deltaTime;
        }
        else if (targetRow != pieceRow)
        {
            tempPosn = new Vector2(targetCol, targetRow);
            transform.position = tempPosn;
            pieceRow = targetRow;
            board.pieceArray[pieceCol, pieceRow] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(firstTouchPosn);
    }

    private void OnMouseUp()
    {
        finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(finalTouchPosn);

        if (Vector2.Distance(firstTouchPosn, finalTouchPosn) > swipeResist)
        {
            CalculateAngle();
        }
    }

    private void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosn.y - firstTouchPosn.y, finalTouchPosn.x - firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && pieceCol < board.boardWidth)
        {  // Right Swipe
            swapPiece = board.pieceArray[pieceCol + 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            swapPiece.GetComponent<Piece>().moveTime = 0f;
            targetCol = pieceCol + 1;
            moveTime = 0;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && pieceRow < board.boardHeight)
        {  // Up Swipe
            swapPiece = board.pieceArray[pieceCol, pieceRow + 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            swapPiece.GetComponent<Piece>().moveTime = 0f;
            targetRow = pieceRow + 1;
            moveTime = 0;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && pieceCol > 0)
        {  // Left Swipe
            swapPiece = board.pieceArray[pieceCol - 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            swapPiece.GetComponent<Piece>().moveTime = 0f;
            targetCol = pieceCol - 1;
            moveTime = 0;
        }
        else if (swipeAngle < -45 && swipeAngle >= -135 && pieceRow > 0)
        {  // Down Swipe
            swapPiece = board.pieceArray[pieceCol, pieceRow - 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            swapPiece.GetComponent<Piece>().moveTime = 0f;
            targetRow = pieceRow - 1;
            moveTime = 0;
        }
    }
}
