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
    [SerializeField] float decreaseRowDelay;

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
                int maxIterations = 0;
                int pieceToUse = Random.Range(0, pieces.Length);
                while(MatchesAt(column, row, pieces[pieceToUse]) && maxIterations < 100)
                {
                    pieceToUse = Random.Range(0, pieces.Length);
                    maxIterations++;
                    Debug.Log(maxIterations);
                }
                maxIterations = 0;
                GameObject piece = Instantiate(pieces[pieceToUse], tempPosn, Quaternion.identity);
                piece.transform.parent = transform;
                piece.name = "Dot(" + column + ", " + row + ")";
                pieceArray[column, row] = piece;
            }
        }
    }

    bool MatchesAt(int col, int row, GameObject piece)
    {
        if (col > 1 && row > 1)
        {
            if (pieceArray[col - 1, row].tag == piece.tag && pieceArray[col - 2, row].tag == piece.tag)
            {
                return true;
            }
            if (pieceArray[col, row - 1].tag == piece.tag && pieceArray[col, row - 2].tag == piece.tag)
            {
                return true;
            }
        }
        else if (col <= 1 || row <= 1)
        {
            if (col > 1)
            {
                if (pieceArray[col - 1, row].tag == piece.tag && pieceArray[col - 2, row].tag == piece.tag)
                {
                    return true;
                }
            }
            if (row > 1)
            {
                if (pieceArray[col, row - 1].tag == piece.tag && pieceArray[col, row - 2].tag == piece.tag)
                {
                    return true;
                }
            }
        }
        return false;
    }

    void DestroyMatchesAt(int col, int row)
    {
        if (pieceArray[col, row].GetComponent<Piece>().isMatched)
        {
            Destroy(pieceArray[col, row]);
            pieceArray[col, row] = null;
        }
    }

    public void DestroyAllMatches()
    {
        for(int row = 0; row < boardHeight; row++)
        {
            for (int col = 0; col < boardWidth; col++)
            {
                if (pieceArray[col, row] != null)
                {
                    DestroyMatchesAt(col, row);
                }
            }
        }
        StartCoroutine(DecreaseRow());
    }

    IEnumerator DecreaseRow()
    {
        int nullCount = 0;
        for (int col = 0; col < boardWidth; col++)
        {
            for (int row = 0; row < boardHeight; row++)
            {
                if(pieceArray[col, row] == null)
                {
                    nullCount++;
                }
                else if(nullCount > 0)
                {
                    pieceArray[col, row].GetComponent<Piece>().pieceRow -= nullCount;
                }
            }
            nullCount = 0;
        }
        yield return new WaitForSeconds(decreaseRowDelay);
    }
}
