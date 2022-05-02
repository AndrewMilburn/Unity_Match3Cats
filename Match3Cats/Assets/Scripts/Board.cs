//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int m_height;
    public int m_width;
    Tile[,] m_grid;
    [SerializeField] GameObject m_tile;
    [SerializeField] GameObject[] m_pieceArray;
    public GameObject[,] m_boardArray;
    public float m_swapDuration;
    public float m_checkMoveDelay;
    int m_maxIterations = 100;
    public float m_decreaseRowDelay;
    public float m_RefillBoardDelay;
    FindMatches m_FindMatches;

    // Start is called before the first frame update
    void Start()
    {
        m_grid = new Tile[m_width, m_height];
        m_boardArray = new GameObject[m_width, m_height];
        SetUpGrid();
        m_FindMatches = FindObjectOfType<FindMatches>();
    }

    void SetUpGrid()
    {
        for(int i = 0; i < m_width; i++)
        {
            for (int j = 0; j < m_height; j++)
            {
                Vector2 tempPosn = new Vector2(i, j);
                GameObject gridSquare = Instantiate(m_tile, tempPosn, Quaternion.identity);
                gridSquare.transform.parent = this.transform;
                gridSquare.name = "Grid Square(" + i + ", " + j + ")";
                int pieceToUse = Random.Range(0, m_pieceArray.Length);
                int iteration = 0;
                while(MatchesAt(i, j, m_pieceArray[pieceToUse]) && iteration < m_maxIterations)
                {
                    pieceToUse = Random.Range(0, m_pieceArray.Length);
                    iteration++;
                }
                iteration = 0;
                GameObject piece = Instantiate(m_pieceArray[pieceToUse], tempPosn, Quaternion.identity);
                piece.transform.parent = this.transform;
                piece.name = "(" + i + ", " + j + ")";
                m_boardArray[i, j] = piece;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool MatchesAt(int column, int row, GameObject piece)
    {
        if(column > 1 && row > 1)
        {
            if(m_boardArray[column - 1, row].tag == piece.tag && m_boardArray[column - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (m_boardArray[column, row - 1].tag == piece.tag && m_boardArray[column, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if(column <= 1 || row <= 1)
        {
            if(column > 1)
            {
                if(m_boardArray[column - 1, row].tag == piece.tag && m_boardArray[column-2, row].tag == piece.tag)
                {
                    return true;
                }
            }
            if(row > 1)
            {
                if (m_boardArray[column, row - 1].tag == piece.tag && m_boardArray[column, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void DestroyMatchesAt(int column, int row)
    {
        if(m_boardArray[column, row].GetComponent<Piece>().m_isMatched)
        {
            m_FindMatches.m_currentMatches.Remove(m_boardArray[column, row]);
            Destroy(m_boardArray[column, row]);
            m_boardArray[column, row] = null;
        }
    }

    public void DestroyAllMatches()
    {
        for (int i = 0; i < m_width; i++)
        {
            for (int j = 0; j < m_height; j++)
            {
                if(m_boardArray[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }
            }
        }
        StartCoroutine(CoDecreaseRow());
    }

    IEnumerator CoDecreaseRow()
    {
        int nullCount = 0;
        for (int i = 0; i < m_width; i++)
        {
            for (int j = 0; j < m_height; j++)
            {
                if (m_boardArray[i, j] == null)
                {
                    nullCount++;
                }
                else if (nullCount > 0)
                {
                    m_boardArray[i, j].GetComponent<Piece>().m_targetPosn.y -= nullCount;
                    m_boardArray[i, j] = null;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(m_decreaseRowDelay);
        //StartCoroutine(CoRefillBoard());
    }

    //void ReplenishPieces()
    //{
    //    for (int i = 0; i < m_width; i++)
    //    {
    //        for (int j = 0; j < m_height; j++)
    //        {
    //            if(m_boardArray[i, j] == null)
    //            {
    //                Vector2 tempPosn = new Vector2(i, j);
    //                int pieceToUse = Random.Range(0, m_pieceArray.Length);
    //                GameObject piece = Instantiate(m_pieceArray[pieceToUse], tempPosn, Quaternion.identity);
    //                m_boardArray[i, j]= piece;
    //            }
    //        }
    //    }
    //}

    //bool MatchesOnBoard()
    //{
    //    for (int i = 0; i < m_width; i++)
    //    {
    //        for (int j = 0; j < m_height; j++)
    //        {
    //            if(m_boardArray[i, j] != null)
    //            {
    //                if(m_boardArray[i, j].GetComponent<Piece>().m_isMatched)
    //                {
    //                    return true;
    //                }
    //            }
    //        }
    //    }
    //    return false;
    //}

    //IEnumerator CoRefillBoard()
    //{
    //    ReplenishPieces();
    //    yield return new WaitForSeconds(m_RefillBoardDelay);

    //    while(MatchesOnBoard())
    //    {
    //        yield return new WaitForSeconds(m_RefillBoardDelay);
    //        DestroyAllMatches();
    //    }
    //}


}
