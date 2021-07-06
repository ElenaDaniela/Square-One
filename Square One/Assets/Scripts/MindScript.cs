using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindScript : MonoBehaviour
{
    public GameObject[] Players;
    [SerializeField] GameObject CurrentPlayer;
    
    
    

    private void Start()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Debug.Log(Players[i].tag);
            if (Players[i] != CurrentPlayer)
            {
                Players[i].GetComponent<PlayerController>().enabled = false;
                if (Players[i].GetComponent<PlayerController>().isGrounded == true)
                    Players[i].GetComponent<Rigidbody2D>().constraints =
                        RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                else
                    Players[i].GetComponent<Rigidbody2D>().constraints =
                        RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                
            }
            
        }
    }

    public void changePlayer(GameObject player)
    {
        CurrentPlayer.GetComponent<PlayerController>().enabled = false;
        CurrentPlayer.GetComponent<Rigidbody2D>().constraints =
            RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        if (CurrentPlayer.GetComponent<PlayerController>().isGrounded == true)
            CurrentPlayer.GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            CurrentPlayer.GetComponent<Rigidbody2D>().constraints =
                RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        CurrentPlayer = player;
    }
}
