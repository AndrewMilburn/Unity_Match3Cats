using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Vector2 m_firstTouchPosn;
    Vector2 m_finalTouchPosn;
    float m_swipeAngle = 0;
    Board m_board;
    public int m_column;
    public int m_row;
    GameObject otherPiece;
    int m_targetColumn;
    int m_targetRow;
    int m_startColumn;
    int m_startRow;
    
    float m_swapTime = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        m_board = FindObjectOfType<Board>();
        m_targetColumn = (int)transform.position.x;
        m_targetRow = (int)transform.position.y;
        m_row = m_targetRow;
        m_column = m_targetColumn;
    }

    // Update is called once per frame
    void Update()
    {
        m_targetColumn = m_column;
        m_targetRow = m_row;
        
        if(Mathf.Abs(m_targetColumn - transform.position.x) > .1)
        {
            Vector2 tempPosn = new Vector2(Mathf.Lerp(m_startColumn, m_targetColumn, m_swapTime / m_board.m_swapDuration), transform.position.y);
            transform.position = tempPosn;
            m_swapTime += Time.deltaTime;
        }
        else
        {
            Vector2 tempPosn = new Vector2(m_targetColumn, transform.position.y);
            transform.position = tempPosn;
            m_board.m_boardArray[m_column, m_row] = this.gameObject;
        }
        if (Mathf.Abs(m_targetRow - transform.position.y) > .1)
        {
            Vector2 tempPosn = new Vector2(transform.position.x, Mathf.Lerp(m_startRow, m_targetRow, m_swapTime / m_board.m_swapDuration));
            transform.position = tempPosn;
            m_swapTime += Time.deltaTime;
        }
        else
        {
            Vector2 tempPosn = new Vector2(transform.position.x, m_targetRow);
            transform.position = tempPosn;
            m_board.m_boardArray[m_column, m_row] = this.gameObject;
        }
    }

    private void OnMouseDown()
    {
        m_firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        m_finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        m_swipeAngle = Mathf.Atan2(m_finalTouchPosn.y - m_firstTouchPosn.y, m_finalTouchPosn.x - m_firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(m_swipeAngle);
        MovePieces();
    }

    void MovePieces()
    {
        if(m_swipeAngle > -45 && m_swipeAngle <= 45 && m_column < m_board.m_width)    // Right Swipe
        {
            otherPiece = m_board.m_boardArray[m_column + 1, m_row];
            otherPiece.GetComponent<Piece>().m_startColumn = otherPiece.GetComponent<Piece>().m_column;
            otherPiece.GetComponent<Piece>().m_column -= 1;
            m_startColumn = m_column;
            m_column += 1;
            m_swapTime = 0;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
        }
        else if(m_swipeAngle > 45 && m_swipeAngle <= 135 && m_row < m_board.m_height)   // Up Swipe
        {
            otherPiece = m_board.m_boardArray[m_column, m_row + 1];
            otherPiece.GetComponent<Piece>().m_startRow = otherPiece.GetComponent<Piece>().m_row;
            otherPiece.GetComponent<Piece>().m_row -= 1;
            m_startRow = m_row;
            m_row += 1;
            m_swapTime = 0;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
        }
        else if((m_swipeAngle > 135 || m_swipeAngle <= -135) && m_column > 0)   // Left Swipe
        {
            otherPiece = m_board.m_boardArray[m_column - 1, m_row];
            otherPiece.GetComponent<Piece>().m_startColumn = otherPiece.GetComponent<Piece>().m_column;
            otherPiece.GetComponent<Piece>().m_column += 1;
            m_startColumn = m_column;
            m_column -= 1;
            m_swapTime = 0;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
        }
        else if(m_swipeAngle < -45 && m_swipeAngle >= -135 && m_row > 0)    // Down Swipe
        {
            otherPiece = m_board.m_boardArray[m_column, m_row - 1];
            otherPiece.GetComponent<Piece>().m_startRow = otherPiece.GetComponent<Piece>().m_row;
            otherPiece.GetComponent<Piece>().m_row += 1;
            m_startRow = m_row;
            m_row -= 1;
            m_swapTime = 0;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
        }
    }
}
