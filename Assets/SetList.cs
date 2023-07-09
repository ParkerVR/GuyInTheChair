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
  [SerializeField] private GameObject ddrUI;
  [SerializeField] private GameObject simonUI;
  [SerializeField] private AudioSource TheSong;


  // Start is called before the first frame update
  void Start() { 
    SetDataList = new List<SetData>();
    SetDataList.Add(new SetData("HOME", 0, 8));
    SetDataList.Add(new SetData("TYPING", 8, 128)); // We should make this longer
    SetDataList.Add(new SetData("DDR", 128, 192));
    SetDataList.Add(new SetData("ENCORE",  192,420));
    //SetDataList.Add(new SetData("SIMON", 192, 420)); // Great start timing , seems to run long? but maybe clicks
    //SetDataList.Add(new SetData("ENCORE", 420, 450));
    //SetDataList.Add(new SetData("TYPING", 8, 12)); // TEST TIMNIg
    //SetDataList.Add(new SetData("DDR", 12, 48)); // TEST TIMING
    //SetDataList.Add(new SetData("SIMON", 48, 256)); // TEST TIMING
    //SetDataList.Add(new SetData("DDR", 256, 320)); // Great start timing
    //SetDataList.Add(new SetData("EQ", 320, 420)); // Great start timing

    DDR.disableDDR();
    
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
  public int gotAHitIterator;

  public List<string> gotAHitWords;
  // Update is called once per frame
  void Update() { 
    gameTimer.iterateTimer();
    kbd.Update();
    //TerminalTMP.text = kbd.getMostRecentKey();

    // SET TERMINAL TEXT 
    if (gameTimer.beatSum < SetDataList[0].beatEnd) {
      TerminalTMP.text = "You are working as an event manager at the mixing table to keep the show afloat.\n\n\n";
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
        ddrGame = new DDR(SetDataList[2].beatStart, SetDataList[2].beatEnd);
        currentSet = SetDataList[2];
        gotAHitIterator = 0;
        gotAHitWords = new List<string> {"Arrow keys to scratch!", "Great!", "Keep it up!", "On fire!", "I love this song!", "Scratch that record!", "Oh yeah.", "Love it!", "Keep going!", "Awesomesauce", "blast off!!", "Keep it poppin'", "Noice", "You're a Hero!!!"};
      } 
      
      if(currentSet == SetDataList[2]) {
        // Process on new beat start
        if (gameTimer.wasLastFrameBeat){
          if (isCurrentBeatLive == false) {
            ddrGame.Misses += 1;
          }
          //isCurrentBeatLive = true;
          kbd.resetCache();
        }
        
        //if (isCurrentBeatLive) {
          Debug.Log($"Making Hit Process with {kbd.getMostRecentKey()} and {gameTimer.beatSum}");
          string hitBeatProcess = ddrGame.HitBeatProcess(kbd.getMostRecentKey(), gameTimer.beatSum);
          if (hitBeatProcess != "none") {
            Debug.Log(hitBeatProcess);
          }
          //TerminalTMP.text += hitBeatProcess;
          if (hitBeatProcess == "hit") {
            ddrGame.Hits += 1-gameTimer.calculateDriftPercentage();
            Debug.Log(1-gameTimer.calculateDriftPercentage());
            isCurrentBeatLive = false;
            gotAHitIterator = Random.Range(0,gotAHitWords.Count);
          } if (hitBeatProcess == "miss") {
            ddrGame.Misses += 1;
            isCurrentBeatLive = false;
          }

        //}
        
        //TerminalTMP.text += "CurrentDirectionToHit:\n";
        //TerminalTMP.text += ddrGame.GetBeatDirection(gameTimer.beatSum);
        ddrGame.RenderDDRArrows(gameTimer.beatSum);
        Debug.Log(ddrGame.RenderDDRString(gameTimer.beatSum));
        //TerminalTMP.text += "LastKeypress:\n";
        //TerminalTMP.text += kbd.getMostRecentKey();
        TerminalTMP.text = $"{gotAHitWords[gotAHitIterator]}\n\n\n\n\n";

      }
    } /*else if (gameTimer.beatSum < SetDataList[3].beatEnd) {
      // SIMON SAYS
      //init
      if(currentSet != SetDataList[3]){
        currentSet = SetDataList[3];
        DDR.disableDDR();
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

    }*/ else if (gameTimer.beatSum < SetDataList[3].beatEnd) {
      DDR.disableDDR();

      TerminalTMP.text = "Comment on Itch if you want an encore\n\n\n\n\n!";
      if(currentSet != SetDataList[3]){
        currentSet = SetDataList[3];
      }
    }
    /* BEAT DEBUG TOOLS
    TerminalTMP.text += '\n';
    TerminalTMP.text += gameTimer.beatSum;
    TerminalTMP.text += '\n';
    TerminalTMP.text += gameTimer.beatLength;
    */
  }
}


