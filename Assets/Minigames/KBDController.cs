using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBDController
{

  public ArrayList keys_pressed;

  // Update is called once per frame from the setlist
  public void Update() {
    keys_pressed = new ArrayList();
    foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
    {
      if (Input.GetKeyDown(kcode))
        keys_pressed.Add(kcode);
    }
    if(Input.mouseScrollDelta.y >0){
      keys_pressed.Add("ScrollUp");
    }
    else if(Input.mouseScrollDelta.y>0){
      keys_pressed.Add("ScrollDown");
    }
  }

}
