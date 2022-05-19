//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    isWaiting,
    isMoving
}

public class Board : MonoBehaviour
{
    public int boardHeight;
    public int boardWidth;
    Tile[,] tileGrid;
    [SerializeField] GameObject tile;
    //[SerializeField] GameObject[] m_pieceArray;
    //public GameObject[,] m_boardArray;
    //public float m_swapDuration;
    //public float m_checkMoveDelay;
    //int m_maxIterations = 100;
    //public float m_decreaseRowDelay;
    //public float m_RefillBoardDelay;
    //FindMatches m_FindMatches;
    //public GameState m_currentState = GameState.isMoving;

    //Start is called before the first frame update
    void Start()
    {
        tileGrid = new Tile[boardWidth, boardHeight];
    //    m_boardArray = new GameObject[m_width, m_height];
        SetUpGrid();
    //    m_FindMatches = FindObjectOfType<FindMatches>();
    }

    void SetUpGrid()
    {
        for (int row = 0; row < boardHeight; row++)
        {
            for (int column = 0; column < boardWidth; row++)
            {
                Vector2 tempPosn = new Vector2(column, row);
                GameObject gridSquare = Instantiate(tile, tempPosn, Quaternion.identity);
				//            gridSquare.transform.parent = this.transform;
				//            gridSquare.name = "Grid Square(" + i + ", " + j + ")";
				//            int pieceToUse = Random.Range(0, m_pieceArray.Length);
				//            int iteration = 0;
				//            while(MatchesAt(i, j, m_pieceArray[pieceToUse]) && iteration < m_maxIterations)
				//            {
				//                pieceToUse = Random.Range(0, m_pieceArray.Length);
				//                iteration++;
				//            }
				//            iteration = 0;
				//            GameObject piece = Instantiate(m_pieceArray[pieceToUse], tempPosn, Quaternion.identity);
				//            piece.transform.parent = this.transform;
				//            piece.name = "(" + i + ", " + j + ")";
				//            m_boardArray[i, j] = piece;
			}
		}
	}

        //// Update is called once per frame
        //void Update()
        //{

        //}

        //bool MatchesAt(int column, int row, GameObject piece)
        //{
        //    if(column > 1 && row > 1)
        //    {
        //        if(m_boardArray[column - 1, row].tag == piece.tag && m_boardArray[column - 2, row].tag == piece.tag)
        //        {
        //            return true;
        //        }
        //        if (m_boardArray[column, row - 1].tag == piece.tag && m_boardArray[column, row - 2].tag == piece.tag)
        //        {
        //            return true;
        //        }
        //    }
        //    else if(column <= 1 || row <= 1)
        //    {
        //        if(column > 1)
        //        {
        //            if(m_boardArray[column - 1, row].tag == piece.tag && m_boardArray[column-2, row].tag == piece.tag)
        //            {
        //                return true;
        //            }
        //        }
        //        if(row > 1)
        //        {
        //            if (m_boardArray[column, row - 1].tag == piece.tag && m_boardArray[column, row - 2].tag == piece.tag)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        //void DestroyMatchesAt(int column, int row)
        //{
        //    if(m_boardArray[column, row].GetComponent<Piece>().m_isMatched)
        //    {
        //        Debug.Log("Destroying " + column + ", " + row);
        //        m_FindMatches.m_currentMatches.Remove(m_boardArray[column, row]);
        //        Destroy(m_boardArray[column, row]);
        //        m_boardArray[column, row] = null;
        //    }
        //}

        //public void DestroyAllMatches()
        //{
        //    for (int i = 0; i < m_width; i++)
        //    {
        //        for (int j = 0; j < m_height; j++)
        //        {
        //            if(m_boardArray[i, j] != null)
        //            {
        //                DestroyMatchesAt(i, j);
        //            }
        //        }
        //    }
        //    StartCoroutine(CoDecreaseRow());
        //}

        //IEnumerator CoDecreaseRow()
        //{
        //    yield return new WaitForSeconds(m_decreaseRowDelay);
        //    Debug.Log("In CoDecreaseRow");
        //    int nullCount = 0;
        //    for (int i = 0; i < m_width; i++)
        //    {
        //        for (int j = 0; j < m_height; j++)
        //        {
        //            if (m_boardArray[i, j] == null)
        //            {
        //                nullCount++;
        //                Debug.Log("Null count: " + "i " + i + ", j " + j + ": " + nullCount);
        //            }
        //            else if (nullCount > 0)
        //            {
        //                Debug.Log("Decreasing " + i + ", " + j + " by " + nullCount);
        //                m_boardArray[i, j].GetComponent<Piece>().m_targetPosn.y -= nullCount;

        //                m_boardArray[i, j] = null;
        //            }
        //        }
        //        nullCount = 0;
        //    }
        //    Debug.Log("Out of CoDecreaseRow Loop");
        //    yield return new WaitForSeconds(m_decreaseRowDelay);
        //    Debug.Log("Out of CoDecreaseRow After Delay");
        //    //StartCoroutine(CoRefillBoard());
        //}

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
