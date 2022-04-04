using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    Vector2 m_firstTouchPosn;
    Vector2 m_finalTouchPosn;
    float m_swipeAngle = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        m_firstTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        m_finalTouchPosn = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    private void CalculateAngle()
    {
        m_swipeAngle = Mathf.Atan2(m_finalTouchPosn.y - m_firstTouchPosn.y, m_finalTouchPosn.x - m_firstTouchPosn.x) * 180 / Mathf.PI;
        Debug.Log(m_swipeAngle);
    }
}
