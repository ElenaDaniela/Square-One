using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
   public MindScript mind; 

   void OnMouseDown()
   {
      mind.changePlayer(this.gameObject);
      GetComponent<PlayerController>().enabled = true;
   }
}
