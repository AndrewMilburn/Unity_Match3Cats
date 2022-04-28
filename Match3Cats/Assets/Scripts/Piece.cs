using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [Header("Board Variables")]
    Board m_board;
    public Vector2 m_position;
    public bool m_isMatched = false;

    [Header("Swipe Variables")]
    Vector2 m_firstTouchPosn;
    Vector2 m_finalTouchPosn;
    float m_swipeAngle = 0;
    GameObject otherPiece;
    public Vector2 m_targetPosn;
    //public Vector2 m_startPosn;
    Vector2 m_previousPosn;
    float m_swapTime = 0;
    float m_swipeResist = .5f;
    

    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Piece Start");
        m_board = FindObjectOfType<Board>();
        m_targetPosn = transform.position;
        m_position = m_targetPosn;
        m_previousPosn = m_position;
        //m_startPosn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        FindMatches();
        if (m_isMatched)
        {
            SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
            mySprite.color = new Color(1f, 1f, 1f, .2f);
        }

        //m_targetPosn = m_position;

        if(Vector2.Distance(transform.position, m_targetPosn) > 0.1f)
        //if (m_swapTime < m_board.m_swapDuration)
        {            
            transform.position = Vector2.Lerp(m_position, m_targetPosn, m_swapTime / m_board.m_swapDuration);
            m_swapTime += Time.deltaTime;
            //if(m_board.m_boardArray[(int)m_position.x, (int)m_position.y] != this.gameObject)
            //{
            //    m_board.m_boardArray[(int)m_position.x, (int)m_position.y] = this.gameObject;
            //}
        }
        else
        {
            transform.position = m_targetPosn;
            m_board.m_boardArray[(int)m_position.x, (int)m_position.y] = this.gameObject;
            this.name = "(" + m_position.x.ToString() + ", " + m_position.y.ToString() + ")";
            m_swapTime = 0;
            m_position = transform.position;
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
        if (Mathf.Abs(m_finalTouchPosn.y - m_firstTouchPosn.y) > m_swipeResist || Mathf.Abs(m_finalTouchPosn.x - m_firstTouchPosn.x) > m_swipeResist)
        {
            m_swipeAngle = Mathf.Atan2(m_finalTouchPosn.y - m_firstTouchPosn.y, m_finalTouchPosn.x - m_firstTouchPosn.x) * 180 / Mathf.PI;
            MovePieces();
        }
    }

    void MovePieces()
    {
        if(m_swipeAngle > -45 && m_swipeAngle <= 45 && m_position.x < m_board.m_width - 1)    // Right Swipe
        {
            otherPiece = m_board.m_boardArray[(int)m_position.x + 1, (int)m_position.y];
            //otherPiece.GetComponent<Piece>().m_position = otherPiece.GetComponent<Piece>().m_position;
            otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.left;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
            m_targetPosn += Vector2.right;
            //m_position = m_position + Vector2.right;
            m_swapTime = 0;
        }
        else if(m_swipeAngle > 45 && m_swipeAngle <= 135 && m_position.y < m_board.m_height - 1)   // Up Swipe
        {
            otherPiece = m_board.m_boardArray[(int)m_position.x, (int)m_position.y + 1];
            //otherPiece.GetComponent<Piece>().m_startPosn = otherPiece.GetComponent<Piece>().m_position;
            otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.down;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
            m_targetPosn += Vector2.up;
            //m_position = m_position + Vector2.up;
            m_swapTime = 0;
        }
        else if((m_swipeAngle > 135 || m_swipeAngle <= -135) && (int)m_position.x > 0)   // Left Swipe
        {
            otherPiece = m_board.m_boardArray[(int)m_position.x - 1, (int)m_position.y];
            //otherPiece.GetComponent<Piece>().m_startPosn = otherPiece.GetComponent<Piece>().m_position;
            otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.right;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
            m_targetPosn += Vector2.left;
            //m_position = m_position + Vector2.left;
            m_swapTime = 0;
        }
        else if(m_swipeAngle < -45 && m_swipeAngle >= -135 && m_position.y > 0)    // Down Swipe
        {
            otherPiece = m_board.m_boardArray[(int)m_position.x, (int)m_position.y - 1];
            //otherPiece.GetComponent<Piece>().m_startPosn = otherPiece.GetComponent<Piece>().m_position;
            otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.up;
            otherPiece.GetComponent<Piece>().m_swapTime = 0;
            m_targetPosn += Vector2.down;
            //m_position = m_position + Vector2.down;
            m_swapTime = 0;
        }

        StartCoroutine(CheckMove());
    }

    void FindMatches()
    {
        if (this != null)
        {
            if (m_position.x > 0 && m_position.x < m_board.m_width - 1)
            {
                GameObject leftPiece1 = m_board.m_boardArray[(int)m_position.x - 1, (int)m_position.y];
                GameObject rightPiece1 = m_board.m_boardArray[(int)m_position.x + 1, (int)m_position.y];
                if (leftPiece1 != null && rightPiece1 != null)
                {
                    if (leftPiece1.tag == this.gameObject.tag && rightPiece1.tag == this.gameObject.tag)
                    {
                        leftPiece1.GetComponent<Piece>().m_isMatched = true;
                        rightPiece1.GetComponent<Piece>().m_isMatched = true;
                        m_isMatched = true;
                    }
                }
            }
            if (m_position.y > 0 && m_position.y < m_board.m_height - 1)
            {
                GameObject upPiece1 = m_board.m_boardArray[(int)m_position.x, (int)m_position.y + 1];
                GameObject downPiece1 = m_board.m_boardArray[(int)m_position.x, (int)m_position.y - 1];
                if (upPiece1 != null && downPiece1 != null)
                {
                    if (upPiece1.tag == this.gameObject.tag && downPiece1.tag == this.gameObject.tag)
                    {
                        upPiece1.GetComponent<Piece>().m_isMatched = true;
                        downPiece1.GetComponent<Piece>().m_isMatched = true;
                        m_isMatched = true;
                    }
                }
            }
        }
    }

    IEnumerator CheckMove()
    {
        yield return new WaitForSeconds(m_board.m_checkMoveDelay);
        if(otherPiece != null)
        {
            if(!m_isMatched && !otherPiece.GetComponent<Piece>().m_isMatched)
            {
                //otherPiece.GetComponent<Piece>().m_startPosn = otherPiece.GetComponent<Piece>().m_position;
                otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_previousPosn;
                otherPiece.GetComponent<Piece>().m_swapTime = 0;
                //m_startPosn = m_position;
                m_targetPosn = m_previousPosn;
                m_swapTime = 0;
            }
            else
            {
                m_board.DestroyAllMatches();
            }
            otherPiece = null;
        }
    }
}
