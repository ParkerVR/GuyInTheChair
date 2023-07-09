using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBDController
{

  public ArrayList keys_pressed;

  public string cachedKey = "NO INPUT"; 
  public string getMostRecentKey (){
    if (keys_pressed.Count > 0) {
      cachedKey = (string)(keys_pressed[0]);
    }
    return cachedKey;
  }

  // Update is called once per frame from the setlist
  public void Update() {
    keys_pressed = new ArrayList();
    foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
    {
      if (Input.GetKeyDown(kcode))
        keys_pressed.Add((kcode).ToString());
    }
    if(Input.mouseScrollDelta.y > 0){
      keys_pressed.Add("ScrollUp");
    }
    else if(Input.mouseScrollDelta.y < 0){
      keys_pressed.Add("ScrollDown");
    }
  }

}
