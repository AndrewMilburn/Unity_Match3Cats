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
        //Debug.Log("In Piece:Start");
        board = FindObjectOfType<Board>();
        targetCol = (int)transform.position.x;
        targetRow = (int)transform.position.y;
        pieceCol = targetCol;
        pieceRow = targetRow;
        previousColumn = pieceCol;
        previousRow = pieceRow;
        findMatches = FindObjectOfType<FindMatches>();
    }

    private void Update()
    {
        //Debug.Log("In Piece:Update");
        //findMatches.FindAllMatches();
        if(isMatched)
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
            moveTime = 0f;
            findMatches.FindAllMatches();
        }
        if (Mathf.Abs(targetRow - transform.position.y) > 0.01f)
        {
            tempPosn = new Vector2(pieceCol, Mathf.Lerp((float)pieceRow, (float)targetRow, moveTime / timeToSwap));
            transform.position = tempPosn;
            moveTime += Time.deltaTime;
        }
        else if(targetRow != pieceRow)
        {
            tempPosn = new Vector2(targetCol, targetRow);
            transform.position = tempPosn;
            pieceRow = targetRow;
            board.pieceArray[pieceCol, pieceRow] = this.gameObject;
            moveTime = 0f;
            findMatches.FindAllMatches();
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
        //Debug.Log("In Piece:CalculateAngle");
        swipeAngle = Mathf.Atan2(finalTouchPosn.y - firstTouchPosn.y, finalTouchPosn.x - firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(swipeAngle);
        MovePieces();
    }

    void MovePieces()
    {
        Debug.Log("In Piece:MovePieces");
        if (swipeAngle > -45 && swipeAngle <= 45 && pieceCol < board.boardWidth - 1)    // Right Swipe
        {
            //Debug.Log("Swipe Right");
            swapPiece = board.pieceArray[pieceCol + 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            targetCol = pieceCol + 1;
        }
        else if (swipeAngle > 45 && swipeAngle <= 135 && pieceRow < board.boardHeight - 1)    // Up Swipe
        {
            //Debug.Log("Swipe Up");
            swapPiece = board.pieceArray[pieceCol, pieceRow + 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            targetRow = pieceRow + 1;
        }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && pieceCol > 0)    // Left Swipe
        {
            //Debug.Log("Swipe Left");
            swapPiece = board.pieceArray[pieceCol - 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            targetCol = pieceCol - 1;
        }
        else if (swipeAngle <= -45 && swipeAngle > -135 && pieceRow > 0)    // Down Swipe
        {
            //Debug.Log("Swipe Down");
            swapPiece = board.pieceArray[pieceCol, pieceRow - 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            targetRow = pieceRow - 1;
        }
        StartCoroutine(CoCheckMove());
    }

    IEnumerator CoCheckMove()
    {
        yield return new WaitForSeconds(checkDelay);
        Debug.Log("in Piece:CheckMove");
        if(swapPiece != null)
        {
            if(!isMatched && !swapPiece.GetComponent<Piece>().isMatched)
            {
                Debug.Log("Swap Piece: " + swapPiece.GetComponent<Piece>().name);
                Debug.Log("This Piece: " + name);
                
                swapPiece.GetComponent<Piece>().targetCol = pieceCol;
                swapPiece.GetComponent<Piece>().targetRow = pieceRow;
                targetRow = previousRow;
                targetCol = previousColumn;
            }
            else
            {
                swapPiece.GetComponent<Piece>().previousColumn = swapPiece.GetComponent<Piece>().pieceCol;
                swapPiece.GetComponent<Piece>().previousRow = swapPiece.GetComponent<Piece>().pieceRow;
                previousRow = pieceRow;
                previousColumn = pieceCol;
                board.DestroyAllMatches();
            }
            swapPiece = null;
        }
    }
}

