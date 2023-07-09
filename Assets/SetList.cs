using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class SetData{
  public string name;
  public int beatStart;
  public int beatEnd;
  public object minigame;
  public SetData(string Name, int BeatStart, int BeatEnd) {
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
    TerminalTMP.text = "";
    kbd = new KBDController();
    gameTimer = new Timer();
    //TheSong.Play();

  }

  // Update is called once per frame
  void Update() { 
    gameTimer.iterateTimer();
    kbd.Update();
    TerminalTMP.text = kbd.getMostRecentKey();

    // SET TERMINAL TEXT 
    if (gameTimer.beatSum < SetDataList[0].beatEnd) {
      TerminalTMP.text += "WELCOME TO THE VENUE. YOU WILL NOT GET THE FAME OF THE ROCK STARS TODAY, BUT LITTLE DOES THE CROWD KNOW HOW MUCH YOU CONTRIBUTE";
    } else if (gameTimer.beatSum < SetDataList[1].beatEnd) {
      TerminalTMP.text += "Press your arrow keys to help the starts light up the dance floor!";
    } else if (gameTimer.beatSum < SetDataList[2].beatEnd) {
      TerminalTMP.text += "TYPE ME!!"; // THIS ONE SHOULD WILL GET COMPLICATED
    } else if (gameTimer.beatSum < SetDataList[3].beatEnd) {
      TerminalTMP.text += "Use 1,2,3, and 4 to control the spotlights!";
    } else if (gameTimer.beatSum < SetDataList[4].beatEnd) { 
      TerminalTMP.text += "Let's hit the dance floor again";
    } else if (gameTimer.beatSum < SetDataList[5].beatEnd) {
      TerminalTMP.text += "Move the EQ with your scroll wheel to meet the vibes and close out";
    } else if (gameTimer.beatSum < SetDataList[6].beatEnd) {
      TerminalTMP.text += "Comment on Itch if you want an encore!";
    }
    TerminalTMP.text += '\n';
    TerminalTMP.text += gameTimer.beatSum;
    TerminalTMP.text += '\n';
    TerminalTMP.text += gameTimer.beatLength;
  }
}


