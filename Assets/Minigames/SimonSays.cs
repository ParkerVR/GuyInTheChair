using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimonSays
{

  
  public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
  };
  public List<int> actionSeq;


  public int numberOfBeats; // this grows from 1 to maxNumberBeats during the simon says
  public int startBeat; 
  public int endBeat;
  public int Hits;
  public int Misses;

  // current place player is at in the seq
  public int currentPlayerIndex;

  // current length of the simon says sequence
  public int currentSeqLength;

  // used to track what the action the renderer should show
  public int renderCounter;

  //Plays simon says for the next x beats

  private void setupActionSeq(){
    actionSeq = new List<int>();
    for (int i = 0; i < 5; i++) { // Just to be safe spawn an extra one
      actionSeq.Add(randomActionGenerator.getSimonSaysAction(4));
    }
  }
  public SimonSays(int StartBeat, int EndBeat){
    startBeat = StartBeat;
    endBeat = EndBeat;
    Hits = 0;
    Misses = 0;
    currentSeqLength = 4;
    currentPlayerIndex = 0;
    mode = "simon";
    // gives us 110 beats, generate entire sequence
    setupActionSeq();
    
  }


  public string mode;
  public int buffer_pos;
  public const int buffer_beats = 4;
  public (string, bool) incrementBeat(){
    bool unlock = false;
    string retval = "";
    retval += $"Buffer Pos: {buffer_pos}\n";
    retval += mode;

    if (mode == "simon") {
      retval += $"Simon Says: ";
      retval += actionSeq[renderCounter].ToString();
      retval += $"\nSimon Counter: {renderCounter}";

      if (buffer_pos == 3) {
        renderCounter++;
      }
      buffer_pos++;
      if (buffer_pos == 4){
        buffer_pos=0;
      }

      if (renderCounter == currentSeqLength) {
        renderCounter = 0;
        mode = "buffer_to_player";
        buffer_pos = 0;
        unlock = true;
      }
      
    } else if (mode == "buffer_to_player") {
      retval += $"Now you try it on {buffer_pos}";
      buffer_pos++;
      if (buffer_pos == buffer_beats){
        mode = "player";
        currentPlayerIndex = 0;
        buffer_pos = 0;
      }
    } else if (mode == "player"){
      
      retval += "Please Type: \n";
      retval += actionSeq[renderCounter].ToString();
      retval += $"\n(Simon # - {renderCounter})";
      
      retval += "On beat 4:";
      retval += buffer_pos+1;
      retval += "\n";

      if (buffer_pos == 0) {
        renderCounter++;
        unlock = true;
      }
      buffer_pos++;
      if (buffer_pos == buffer_beats){
        buffer_pos=0;
      }
      if(currentPlayerIndex == currentSeqLength){
        mode = "buffer_to_simon";
        currentPlayerIndex = 0;
        buffer_pos = 0;
      }
    } else if (mode == "buffer_to_simon") {
      retval += $"My turn.. on {buffer_pos}";
      buffer_pos++;
      if (buffer_pos == buffer_beats){
        mode = "simon";
        setupActionSeq();
        currentPlayerIndex = 0;
        buffer_pos = 0;
        unlock = true;
      }
    }
    // make simon says go up by 1 and make the player start at 0
    retval += '\n';
    Debug.Log(retval);
    return (retval, unlock);
  }
  public string HitBeatProcess(string keypress, int currentBeat){
    //Debug.Log(keypress);
    if(keypress == "NO INPUT"){
      return "Wait";
    }

    string targetKeypress = "Alpha"+actionSeq[currentPlayerIndex].ToString();
    Debug.Log($"\nkeyPress={keypress} target={targetKeypress} buffer={buffer_pos}  \n");
    if(keypress == targetKeypress && buffer_pos == 0 && mode == "player"){
      currentPlayerIndex += 1;
      Hits += 1;
      Debug.Log("HITHITHIT\nHITHITHIT\nHITHITHIT");
      return "Hit";
    }
    else{
      Misses += 1;
      return "Miss";
    }
  }


  

  

}
