//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] m_pieceArray;
    
    // Start is called before the first frame update
    void Start()
    {
        InitPieces();
    }

    void InitPieces()
    {
        int pieceToUse = Random.Range(0, m_pieceArray.Length);
        GameObject piece = Instantiate(m_pieceArray[pieceToUse], transform.position, Quaternion.identity);
        piece.transform.parent = this.transform;
        piece.name = this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
