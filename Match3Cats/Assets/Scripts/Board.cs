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

    
}
