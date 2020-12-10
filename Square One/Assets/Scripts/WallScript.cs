using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class WallScript : MonoBehaviour
{
    private TilemapCollider2D tm;
    private BoxCollider2D triggerBox;
    private GameObject tagWall;

    private void Start()
    {
       tm = GetComponent<TilemapCollider2D>();
       tagWall = GameObject.FindGameObjectWithTag("Red");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.CompareTag(tagWall.tag));
        if (other.CompareTag(tagWall.tag))
        {
            tm.enabled = false;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        {
            tm.enabled = true;
        }
    }
}
