using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMatches : MonoBehaviour
{
    Board m_board;
    public List<GameObject> m_currentMatches = new List<GameObject>();
    [SerializeField] float m_findMatchesDelay;

    // Start is called before the first frame update
    void Start()
    {
        m_board = FindObjectOfType<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoFindAllMatches()
    {
        yield return new WaitForSeconds(m_findMatchesDelay);
        //int numMatches = 0;

        for (int i = 0; i < m_board.m_width; i++)
        {
            for (int j = 0; j < m_board.m_height; j++)
            {
                GameObject currentPiece = m_board.m_boardArray[i, j];
                if (currentPiece != null)
                {
                    if(i > 0 && i < m_board.m_width - 1)
                    {
                        GameObject leftPiece = m_board.m_boardArray[i - 1, j];
                        GameObject rightPiece = m_board.m_boardArray[i + 1, j];
                        if(leftPiece != null && rightPiece != null)
                        {
                            if (leftPiece.tag == currentPiece.tag && rightPiece.tag == currentPiece.tag)
                            {
                                if (!m_currentMatches.Contains(leftPiece))
                                {
                                    m_currentMatches.Add(leftPiece);
                                }
                                leftPiece.GetComponent<Piece>().m_isMatched = true;
                                if(!m_currentMatches.Contains(rightPiece))
                                {
                                    m_currentMatches.Add(rightPiece);
                                }
                                rightPiece.GetComponent<Piece>().m_isMatched = true;
                                if(!m_currentMatches.Contains(currentPiece))
                                {
                                    m_currentMatches.Add(currentPiece);
                                }
                                currentPiece.GetComponent<Piece>().m_isMatched = true;
                            }
                        }
                    }
                    if(j > 0 && j < m_board.m_height - 1)
                    {
                        GameObject upPiece = m_board.m_boardArray[i, j + 1];
                        GameObject downPiece = m_board.m_boardArray[i, j - 1];
                        if(upPiece != null && downPiece != null)
                        {
                            if(upPiece.tag == currentPiece.tag && downPiece.tag == currentPiece.tag)
                            {
                                if (!m_currentMatches.Contains(upPiece))
                                {
                                    m_currentMatches.Add(upPiece);
                                }
                                upPiece.GetComponent<Piece>().m_isMatched = true;
                                if (!m_currentMatches.Contains(downPiece))
                                {
                                    m_currentMatches.Add(downPiece);
                                }
                                downPiece.GetComponent<Piece>().m_isMatched = true;
                                if (!m_currentMatches.Contains(currentPiece))
                                {
                                    m_currentMatches.Add(currentPiece);
                                }

                                currentPiece.GetComponent<Piece>().m_isMatched = true;

                            }
                        }
                    }
                }    
            }
        }
    }

    public void FindAllMatches()
    {
        StartCoroutine(CoFindAllMatches());
    }
}
