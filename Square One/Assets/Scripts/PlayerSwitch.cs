using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
   public MindScript mind;
   public GameObject Player1, Player2, Player3;
   

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.Alpha1))
      {
         mind.changePlayer(Player1);
         Player1.GetComponent<PlayerController>().enabled = true;
         Player1.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
      }
      if (Input.GetKeyDown(KeyCode.Alpha2))
      {
         mind.changePlayer(Player2);
         Player2.GetComponent<PlayerController>().enabled = true;
         Player2.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
      }
      if (Input.GetKeyDown(KeyCode.Alpha3))
      {
         mind.changePlayer(Player3);
         Player3.GetComponent<PlayerController>().enabled = true;
         Player3.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
      }
   }
   /*void OnMouseDown()
   {
      mind.changePlayer(this.gameObject);
      GetComponent<PlayerController>().enabled = true;
   }*/
}
