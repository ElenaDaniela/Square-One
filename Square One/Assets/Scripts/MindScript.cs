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
            if(Players[i] != CurrentPlayer)
                Players[i].GetComponent<PlayerController>().enabled = false;
        }
    }

    public void changePlayer(GameObject player)
    {
        CurrentPlayer.GetComponent<PlayerController>().enabled = false;
        CurrentPlayer = player;
    }
}
