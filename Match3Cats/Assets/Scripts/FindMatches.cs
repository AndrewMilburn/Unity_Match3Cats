using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    Board board;
    [SerializeField] List<GameObject> currentMatches = new List<GameObject>();
    [SerializeField] float findMatchDelay;

    private void Start()
    {
        board = FindObjectOfType<Board>();
    }

    IEnumerator CoFindAllMatches()
    {
        Debug.Log("In FindMatches:CoFindAllMatches");
        yield return new WaitForSeconds(findMatchDelay);
        for (int row = 0; row < board.boardHeight; row++)
        {
            for (int col = 0; col < board.boardWidth; col++)
            {
                GameObject currentPiece = board.pieceArray[col, row];
                if(currentPiece != null)
                {
                    if(col > 0 && col < board.boardWidth - 1)
                    {
                        GameObject leftPiece = board.pieceArray[col - 1, row];
                        GameObject rightPiece = board.pieceArray[col + 1, row];
                        if(leftPiece != null && rightPiece != null)
                        {
                            if(leftPiece.tag == currentPiece.tag && rightPiece.tag == currentPiece.tag)
                            {
                                leftPiece.GetComponent<Piece>().isMatched = true;
                                rightPiece.GetComponent<Piece>().isMatched = true;
                                currentPiece.GetComponent<Piece>().isMatched = true;
                                //Debug.Log(currentPiece.tag + " is Matched");
                            }
                        }
                    }
                    if (row > 0 && row < board.boardHeight - 1)
                    {
                        GameObject upPiece = board.pieceArray[col, row + 1];
                        GameObject downPiece = board.pieceArray[col, row - 1];
                        if (upPiece != null && downPiece != null)
                        {
                            if (upPiece.tag == currentPiece.tag && downPiece.tag == currentPiece.tag)
                            {
                                upPiece.GetComponent<Piece>().isMatched = true;
                                downPiece.GetComponent<Piece>().isMatched = true;
                                currentPiece.GetComponent<Piece>().isMatched = true;
                                //Debug.Log(currentPiece.tag + " is Matched");
                            }
                        }
                    }
                }
            }
        }
    }

    public void FindAllMatches()
    {
        //Debug.Log("In FindMatches:FindAllMatches");
        StartCoroutine(CoFindAllMatches());
    }
}

