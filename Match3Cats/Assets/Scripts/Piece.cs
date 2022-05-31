using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour {
    Vector2 firstTouchPosn;
    Vector2 finalTouchPosn;
    float swipeAngle = 0;
    Board board;
    int pieceCol;
    int pieceRow;
    GameObject swapPiece;
    int targetCol;
    int targetRow;

    private void Start() {
        board = FindObjectOfType<Board>();
        targetCol = (int)transform.position.x;
        targetRow = (int)transform.position.y;
        pieceCol = targetCol;
        pieceRow = targetRow;
    }


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
        MovePieces();
    }

    void MovePieces() {
        if (swipeAngle > -45 && swipeAngle <= 45 && pieceCol < board.boardWidth) {  // Right Swipe
            swapPiece = board.pieceGrid[pieceCol + 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            targetCol = pieceCol + 1;
            }
        else if (swipeAngle > 45 && swipeAngle <= 135 && pieceRow < board.boardHeight) {  // Up Swipe
            swapPiece = board.pieceGrid[pieceCol, pieceRow + 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            targetRow = pieceRow + 1;
            }
        else if ((swipeAngle > 135 || swipeAngle <= -135) && pieceCol > 0) {  // Left Swipe
            swapPiece = board.pieceGrid[pieceCol - 1, pieceRow];
            swapPiece.GetComponent<Piece>().targetCol = pieceCol;
            targetCol = pieceCol - 1;
            }
        else if (swipeAngle < -45 && swipeAngle > 135 && pieceRow > 0) {  // Down Swipe
            swapPiece = board.pieceGrid[pieceCol, pieceRow - 1];
            swapPiece.GetComponent<Piece>().targetRow = pieceRow;
            targetRow = pieceRow - 1;
        }
    }
}
