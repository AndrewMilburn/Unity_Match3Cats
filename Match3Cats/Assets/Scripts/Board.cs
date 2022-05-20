//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Board : MonoBehaviour
{
    public int boardHeight;
    public int boardWidth;
    Tile[,] tileGrid;
    [SerializeField] GameObject tile;


    //Start is called before the first frame update
    void Start()
    {
        tileGrid = new Tile[boardWidth, boardHeight];
        SetUpGrid();
    }

    void SetUpGrid()
    {
        for (int row = 0; row < boardHeight; row++)
        {
            for (int column = 0; column < boardWidth; column++)
            {
                Vector2 tempPosn = new Vector2(column, row);
                GameObject gridSquare = Instantiate(tile, tempPosn, Quaternion.identity);
			}
		}
	}
}
