using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    //[Header("Board Variables")]
    //Board m_board;
    //public Vector2 m_position;
    //public bool m_isMatched = false;

    //[Header("Swipe Variables")]
    //Vector2 m_firstTouchPosn;
    //Vector2 m_finalTouchPosn;
    //float m_swipeAngle = 0;
    //GameObject otherPiece;
    //public Vector2 m_targetPosn;
    //Vector2 m_previousPosn;
    //[SerializeField]float m_swapTime;
    //float m_swipeResist = .5f;
    //FindMatches m_findMatches;
    //public float m_distance;
    

    
    //// Start is called before the first frame update
    //void Start()
    //{
    //    m_board = FindObjectOfType<Board>();
    //    m_findMatches = FindObjectOfType<FindMatches>();
    //    m_targetPosn = transform.position;
    //    m_position = m_targetPosn;
    //    m_previousPosn = m_position;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (m_isMatched)
    //    {
    //        SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
    //        mySprite.color = new Color(1f, 1f, 1f, .2f);
    //    }

    //    m_distance = Vector2.Distance(m_targetPosn, transform.position);

    //    if (Mathf.Abs(m_distance) > 0.05f)
    //    {
    //        transform.position = Vector2.Lerp(m_position, m_targetPosn, m_swapTime / m_board.m_swapDuration);
    //        m_swapTime += Time.deltaTime;
    //    }
    //    else
    //    {
    //        transform.position = m_targetPosn;
    //        m_board.m_boardArray[(int)m_position.x, (int)m_position.y] = this.gameObject;
    //        m_swapTime = 0;
    //        m_position = transform.position;
    //        m_previousPosn = m_position;
    //        m_findMatches.FindAllMatches();
    //    }
    //}

    //private void OnMouseDown()
    //{
    //    if (m_board.m_currentState == GameState.isWaiting)
    //    {
    //        m_firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    }
    //}

    //private void OnMouseUp()
    //{
    //    if (m_board.m_currentState == GameState.isWaiting)
    //    {
    //        m_finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //        CalculateAngle();
    //    }
    //}

    //private void CalculateAngle()
    //{
    //    if (Mathf.Abs(m_finalTouchPosn.y - m_firstTouchPosn.y) > m_swipeResist || Mathf.Abs(m_finalTouchPosn.x - m_firstTouchPosn.x) > m_swipeResist)
    //    {
    //        m_swipeAngle = Mathf.Atan2(m_finalTouchPosn.y - m_firstTouchPosn.y, m_finalTouchPosn.x - m_firstTouchPosn.x) * 180 / Mathf.PI;
    //        m_board.m_currentState = GameState.isMoving;
    //        MovePieces();
    //    }
    //    else
    //    {
    //        m_board.m_currentState = GameState.isWaiting;
    //    }
    //}

    //void MovePieces()
    //{
    //    if(m_swipeAngle > -45 && m_swipeAngle <= 45 && m_position.x < m_board.m_width - 1)    // Right Swipe
    //    {
    //        otherPiece = m_board.m_boardArray[(int)m_position.x + 1, (int)m_position.y];
    //        otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.left;
    //        otherPiece.GetComponent<Piece>().m_swapTime = 0;
    //        m_targetPosn += Vector2.right;
    //        m_swapTime = 0;
    //    }
    //    else if(m_swipeAngle > 45 && m_swipeAngle <= 135 && m_position.y < m_board.m_height - 1)   // Up Swipe
    //    {
    //        otherPiece = m_board.m_boardArray[(int)m_position.x, (int)m_position.y + 1];
    //        otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.down;
    //        otherPiece.GetComponent<Piece>().m_swapTime = 0;
    //        m_targetPosn += Vector2.up;
    //        m_swapTime = 0;
    //    }
    //    else if((m_swipeAngle > 135 || m_swipeAngle <= -135) && (int)m_position.x > 0)   // Left Swipe
    //    {
    //        otherPiece = m_board.m_boardArray[(int)m_position.x - 1, (int)m_position.y];
    //        otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.right;
    //        otherPiece.GetComponent<Piece>().m_swapTime = 0;
    //        m_targetPosn += Vector2.left;
    //        m_swapTime = 0;
    //    }
    //    else if(m_swipeAngle < -45 && m_swipeAngle >= -135 && m_position.y > 0)    // Down Swipe
    //    {
    //        otherPiece = m_board.m_boardArray[(int)m_position.x, (int)m_position.y - 1];
    //        otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_position + Vector2.up;
    //        otherPiece.GetComponent<Piece>().m_swapTime = 0;
    //        m_targetPosn += Vector2.down;
    //        m_swapTime = 0;
    //    }

    //    StartCoroutine(CoCheckMove());

    //}


    //IEnumerator CoCheckMove()
    //{
    //    yield return new WaitForSeconds(m_board.m_checkMoveDelay);
    //    if(otherPiece != null)
    //    {
    //        if(!m_isMatched && !otherPiece.GetComponent<Piece>().m_isMatched)
    //        {
    //            otherPiece.GetComponent<Piece>().m_targetPosn = otherPiece.GetComponent<Piece>().m_previousPosn;
    //            otherPiece.GetComponent<Piece>().m_swapTime = 0;
    //            m_targetPosn = m_previousPosn;
    //            m_swapTime = 0;
    //        }
    //        else
    //        {
    //            otherPiece.GetComponent<Piece>().m_previousPosn = otherPiece.GetComponent<Piece>().m_position;
    //            m_previousPosn = m_position;
    //            m_board.DestroyAllMatches();
    //        }
    //        otherPiece = null;
    //    }
    //}
}
