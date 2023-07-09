using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class SetData{
  public string name;
  public int beatStart;
  public int beatEnd;
  
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
    SetDataList.Add(new SetData("HOME", 0, 8));
    //SetDataList.Add(new SetData("TYPING", 8, 64)); // We should make this longer
    //SetDataList.Add(new SetData("DDR", 64, 128));
    //SetDataList.Add(new SetData("SIMON", 128, 256)); // Great start timing , seems to run long? but maybe clicks

    SetDataList.Add(new SetData("TYPING", 8, 12)); // TEST TIMNIg
    SetDataList.Add(new SetData("DDR", 12, 16)); // TEST TIMING
    SetDataList.Add(new SetData("SIMON", 16, 256)); // TEST TIMING
    SetDataList.Add(new SetData("DDR", 256, 320)); // Great start timing
    SetDataList.Add(new SetData("EQ", 320, 420)); // Great start timing
    SetDataList.Add(new SetData("ENCORE", 420, 450));

    currentSet = SetDataList[0];
    TerminalTMP.text = "";
    kbd = new KBDController();
    gameTimer = new Timer();
    isCurrentBeatLive = true;

  }
  public Typer typingGame;
  public DDR ddrGame;
  public Typer typerGame;

  public SimonSays SSGame;
  public bool isCurrentBeatLive; // true if input still allowed this beat

  public string cachedSimonString;
 
  // Update is called once per frame
  void Update() { 
    gameTimer.iterateTimer();
    kbd.Update();
    //TerminalTMP.text = kbd.getMostRecentKey();

    // SET TERMINAL TEXT 
    if (gameTimer.beatSum < SetDataList[0].beatEnd) {
      TerminalTMP.text = "WELCOME TO THE VENUE. YOU WILL NOT GET THE FAME OF THE ROCK STARS TODAY, BUT LITTLE DOES THE CROWD KNOW HOW MUCH YOU CONTRIBUTE";
    } else if (gameTimer.beatSum < SetDataList[1].beatEnd) {
      if (gameTimer.wasLastFrameBeat){
        if (isCurrentBeatLive == false) {
          Debug.Log("Should Miss");
        }
        isCurrentBeatLive = true;
      }
      // TYPER MINIGAME
      if (currentSet != SetDataList[1]) {
        currentSet = SetDataList[1];
        typerGame = new Typer(gameTimer.beatSum);
        TerminalTMP.text = Typer.info();
      }
      bool shouldLockout;
      if (isCurrentBeatLive) {
        (TerminalTMP.text, shouldLockout) = typerGame.Update(gameTimer.beatSum, kbd.keypresses);
      } else {
        (TerminalTMP.text, shouldLockout) = typerGame.Update(gameTimer.beatSum, new ArrayList());
      }
      if (shouldLockout) {
        isCurrentBeatLive = false;
      }
    } else if (gameTimer.beatSum < SetDataList[2].beatEnd) {
      // DDR MINIGAME
      if(currentSet != SetDataList[2]){
        currentSet = SetDataList[2];
        ddrGame = new DDR(SetDataList[2].beatStart, SetDataList[2].beatEnd);
      }
      if(currentSet == SetDataList[2]) {
        // Proocess on new beat start
        if (gameTimer.wasLastFrameBeat){
          if (isCurrentBeatLive == false) {
            ddrGame.Misses += 1;
          }
          isCurrentBeatLive = true;
          kbd.resetCache();
        }
        TerminalTMP.text = "Press your arrow keys to help the starts light up the dance floor!\n";
    
        if (isCurrentBeatLive) {
          string hitBeatProcess = ddrGame.HitBeatProcess(kbd.getMostRecentKey(), gameTimer.beatSum);
          if (hitBeatProcess != "none") {
            Debug.Log(hitBeatProcess);
          }
          TerminalTMP.text += hitBeatProcess;
          if (hitBeatProcess == "hit") {
            ddrGame.Hits += 1-gameTimer.calculateDriftPercentage();
            Debug.Log(1-gameTimer.calculateDriftPercentage());
            isCurrentBeatLive = false;
          } if (hitBeatProcess == "miss") {
            ddrGame.Misses += 1;
            isCurrentBeatLive = false;
          }

        }
        
        TerminalTMP.text += "CurrentDirectionToHit:\n";
        TerminalTMP.text += ddrGame.GetBeatDirection(gameTimer.beatSum);
        TerminalTMP.text += ddrGame.RenderDDRString(gameTimer.beatSum);
        TerminalTMP.text += "LastKeypress:\n";
        TerminalTMP.text += kbd.getMostRecentKey();
      }
    } else if (gameTimer.beatSum < SetDataList[3].beatEnd) {
      // SIMON SAYS
      //init
      if(currentSet != SetDataList[3]){
        currentSet = SetDataList[3];
        isCurrentBeatLive = true;
        cachedSimonString = "";
        SSGame = new SimonSays(SetDataList[3].beatStart, SetDataList[3].beatEnd);
      }
      TerminalTMP.text = "Use 1,2,3, and 4 to control the spotlights!\n"; 
      //OnBeat
      if (gameTimer.wasLastFrameBeat){
          
       
        
        kbd.resetCache();
        bool unlock;
        (cachedSimonString, unlock) = SSGame.incrementBeat();
        if (unlock){
          isCurrentBeatLive = true;
        }
        Debug.Log($"\nNew string: {cachedSimonString} Simon Mode: {SSGame.mode}\n");
        
      }
      //per-Frame
      TerminalTMP.text += cachedSimonString;

      if (isCurrentBeatLive & SSGame.mode=="player") {
        if (kbd.getMostRecentKey() != "NO INPUT") {
          string hitBeatProcess = SSGame.HitBeatProcess(kbd.getMostRecentKey(), gameTimer.beatSum);
          Debug.Log(hitBeatProcess);
          isCurrentBeatLive = false;
        }
      }
      TerminalTMP.text += '\n';
      TerminalTMP.text += SSGame.Hits.ToString();

    } else if (gameTimer.beatSum < SetDataList[4].beatEnd) { 
      TerminalTMP.text = "Let's hit the dance floor again";
      if(currentSet != SetDataList[4]){
        currentSet = SetDataList[4];
      }
    } else if (gameTimer.beatSum < SetDataList[5].beatEnd) {
      TerminalTMP.text = "Move the EQ with your scroll wheel to meet the vibes and close out";
      if(currentSet != SetDataList[5]){
        currentSet = SetDataList[5];
      }
    } else if (gameTimer.beatSum < SetDataList[6].beatEnd) {
      TerminalTMP.text = "Comment on Itch if you want an encore!";
      if(currentSet != SetDataList[6]){
        currentSet = SetDataList[6];
      }
    }
    
    TerminalTMP.text += '\n';
    TerminalTMP.text += gameTimer.beatSum;
    TerminalTMP.text += '\n';
    TerminalTMP.text += gameTimer.beatLength;
  }
}


