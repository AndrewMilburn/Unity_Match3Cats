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

    // Start is called before the first frame update
    void Start()
    {
        m_grid = new Tile[m_width, m_height];
        m_boardArray = new GameObject[m_width, m_height];
        SetUpGrid();
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
                    Debug.Log(iteration);
                }
                iteration = 0;
                GameObject piece = Instantiate(m_pieceArray[pieceToUse], tempPosn, Quaternion.identity);
                piece.transform.parent = this.transform;
                piece.name = "Dot (" + i + ", " + j + ")";
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
}
