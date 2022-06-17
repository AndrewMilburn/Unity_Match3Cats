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
    float swipeAngle = 0;
    float swipeResist = 0.5f;
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
        findMatches = FindObjectOfType<FindMatches>();
        previousColumn = pieceCol;
        previousRow = pieceRow;
    }

    private void Update()
    {

        if (isMatched)
        {
            SpriteRenderer pieceSprite = GetComponent<SpriteRenderer>();
            pieceSprite.color = new Color(1f, 1f, 1f, 1f);
        }

        if (Mathf.Abs(targetCol - transform.position.x) > 0.01f)
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
            findMatches.FindAllMatches();
            moveTime = 0;
        }
        if (Mathf.Abs(targetRow - transform.position.y) > 0.01f)
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

            moveTime = 0;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("In MouseDown");

        firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(firstTouchPosn);
    }

    private void OnMouseUp()
    {
        Debug.Log("In MouseUp");

        finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(finalTouchPosn);

        if (Vector2.Distance(firstTouchPosn, finalTouchPosn) > swipeResist)
        {
            CalculateAngle();
        }
    }

    private void CalculateAngle()
    {
        Debug.Log("In CalculateAngle");

        swipeAngle = Mathf.Atan2(finalTouchPosn.y - firstTouchPosn.y, finalTouchPosn.x - firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces()
    {
        Debug.Log("In MovePieces");

        if (swipeAngle > -45 && swipeAngle <= 45 && pieceCol < board.boardWidth - 1)
        {  // Right Swipe
            swapPiece = board.pieceArray[pieceCol + 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            swapPiece.GetComponent<Piece>().moveTime = 0f;
            targetCol = pieceCol + 1;
            moveTime = 0;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && pieceRow < board.boardHeight - 1)
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
        StartCoroutine(CheckMove());
    }

    IEnumerator CheckMove()
    {
        Debug.Log("In CheckMove");

        yield return new WaitForSeconds(checkDelay);
        if(swapPiece != null)
        {
            if(!isMatched && !swapPiece.GetComponent<Piece>().isMatched)
            {
                Debug.Log("Swap Piece " + swapPiece.GetComponent<Piece>().name);
                Debug.Log("This Piece " + name);
                
                swapPiece.GetComponent<Piece>().targetCol = pieceCol;
                swapPiece.GetComponent<Piece>().targetRow = pieceRow;
                targetRow = previousRow;
                targetCol = previousColumn;
            }
            else
            {
                board.DestroyAllMatches();
            }
            swapPiece = null;
        }
    }
}
