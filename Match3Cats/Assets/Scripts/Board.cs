//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Board : MonoBehaviour
{
    public int boardHeight;
    public int boardWidth;
    public GameObject[] pieces;
    [SerializeField] GameObject tile;
    public GameObject[,] pieceArray;

    //Start is called before the first frame update
    void Start()
    {
        pieceArray = new GameObject[boardWidth, boardHeight];
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
                gridSquare.transform.parent = transform;
                gridSquare.name = "Grid Cell(" + column + ", " + row + ")";
                int pieceToUse = Random.Range(0, pieces.Length);
                GameObject piece = Instantiate(pieces[pieceToUse], tempPosn, Quaternion.identity);
                piece.transform.parent = transform;
                piece.name = "Dot(" + column + ", " + row + ")";
                pieceArray[column, row] = piece;
                //Debug.Log(piece.name + pieceGrid[column, row].GetComponent<Piece>().tag);
            }
        }
    }
}
