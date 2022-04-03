using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] int m_height;
    [SerializeField] int m_width;
    Tile[,] m_grid;
    [SerializeField] GameObject m_tile;
    
    // Start is called before the first frame update
    void Start()
    {
        m_grid = new Tile[m_width, m_height];
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
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
