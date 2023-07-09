using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class SetData{
  public string name;
  public float beatStart;
  public float beatEnd;
  public object minigame;
  public SetData(string Name, float BeatStart, float BeatEnd) {
    name = Name;
    beatStart = BeatStart;
    beatEnd = BeatEnd;
  }
  
}

// This class should handle all timeline
public class SetList : MonoBehaviour
{ 
  // Structs 
  public List<SetData> SetDataList;
  
  // Class Variables
  public SetData currentSet;

  // Internal Clients
  public KBDController kbd;

  public Timer gameTimer; 

  // Unity Object Imports
  [SerializeField] private TextMeshProUGUI TerminalTMP;

  [SerializeField] private AudioSource TheSong;


  // Start is called before the first frame update
  void Start() { 
    SetDataList = new List<SetData>();
    SetDataList.Add(new SetData("HOME", 0, 16));
    SetDataList.Add(new SetData("DDR", 16, 64));
    SetDataList.Add(new SetData("TYPING", 64, 128));
    SetDataList.Add(new SetData("SIMON", 128, 256));
    SetDataList.Add(new SetData("DDR", 256, 320));
    SetDataList.Add(new SetData("EQ", 320, 420));
    SetDataList.Add(new SetData("ENCORE", 420, 450));

    currentSet = SetDataList[0];
    TerminalTMP.text = "WELCOME TO THE VENUE. YOU WILL NOT GET THE FAME OF THE ROCK STARS TODAY, BUT LITTLE DOES THE CROWD KNOW HOW MUCH YOU CONTRIBUTE";
    kbd = new KBDController();
    gameTimer = new Timer();
    TheSong.Play();

  }

  // Update is called once per frame
  void Update() { 
    kbd.Update();
    TerminalTMP.text = (string)(kbd.keys_pressed[0]);

    /* Enable this after testing keyboard input
    switch (currentSet) {

      // SET TERMINAL TEXT 
      case Set.HOME: 
        TerminalTMP.text = "WELCOME TO THE VENUE. YOU WILL NOT GET THE FAME OF THE ROCK STARS TODAY, BUT LITTLE DOES THE CROWN KNOW HOW MUCH YOU CONTRIBUTE";
        break; 
      case Set.DDR:
        TerminalTMP.text = "Press your arrow keys to help the starts light up the dance floor!";
        break;
      case Set.TYPING:
        TerminalTMP.text = "TYPE ME!!"; // THIS ONE SHOULD BE MORE COMPLICATED
        break;
      case Set.SIMON:
        TerminalTMP.text = "Use 1,2,3, and 4 to control the spotlights!";
        break;
      case Set.EQ:
        TerminalTMP.text = "Move the EQ with your scroll wheel to meet the vibes and close out";
        break;
      case Set.ENCORE:
        TerminalTMP.text = "Comment on Itch if you want an encore!";
        break;
    }
    */
  }
}


