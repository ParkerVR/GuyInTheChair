using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KBDController
{

  public ArrayList keypresses;

  public string cachedKey = "NO INPUT"; 
  public string getMostRecentKey (){
    if (keypresses.Count > 0) {
      cachedKey = (string)(keypresses[0]);
    }
    return cachedKey;
  }

  public void resetCache() {
    cachedKey = "NO INPUT";
  }

  // Update is called once per frame from the setlist
  public void Update() {
    keypresses = new ArrayList();
    foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
    {
      if (Input.GetKeyDown(kcode))
        keypresses.Add((kcode).ToString());
    }
    if(Input.mouseScrollDelta.y > 0){
      keypresses.Add("ScrollUp");
    }
    else if(Input.mouseScrollDelta.y < 0){
      keypresses.Add("ScrollDown");
    }
  }

}
