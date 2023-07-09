using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDR
{
  public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT,
    NONE
  };

  public int numberOfBeats;
  // List of actions by Beat

  public List<Direction> actionQueue;
  //Plays simon says for the next x beats

  // Hits sums acccuracies (1 is perfect, down to .5)
  public float Hits;
  public int Misses;
  public int startBeat;
  public int beatSum;

  public DDR(int StartBeat, int BeatSum){
    Hits = 0;
    Misses = 0;
    startBeat = StartBeat;
    beatSum= BeatSum;
    actionQueue=new List<Direction>();
    // Add start buffer
    for (int i = 0; i < 8; i++) {
      actionQueue.Add(Direction.NONE);
    }
    numberOfBeats=beatSum - startBeat;
    // Generate entire action queue
    for (int i = 8; i < numberOfBeats; i++) { 
      int howManyButtonsRoll = Random.Range(0,2);
      if (howManyButtonsRoll == 0) {
        actionQueue.Add(Direction.NONE);
      } else {
        actionQueue.Add(randomActionGenerator.getDDRAction());
      }
    } 
  }

  // Call this every frame
  public string HitBeatProcess(string keypress, int currentBeat){
    Direction dirThisBeat = GetBeatDirection(currentBeat + 1);
    //Debug.Log(dirThisBeat);
    if (dirThisBeat != Direction.NONE) {
      if (keypress != "NO INPUT") {
        //Debug.Log($"Direction: {dirThisBeat}, keyPress = {keypress}");
        if (dirThisBeat == Direction.UP) {
          if (keypress=="UpArrow") {
            return "hit";
          } else {return "miss";}
        } else if (dirThisBeat == Direction.DOWN) {
          if (keypress=="DownArrow") {
            return "hit";
          } else {return "miss";}
        } else if (dirThisBeat == Direction.LEFT) {
          if (keypress=="LeftArrow") {
            return "hit";
          } else {return "miss";}
        } else if (dirThisBeat == Direction.RIGHT) {
          if (keypress=="RightArrow") {
            return "hit";
          } else {return "miss";}
        }
      }
    }
    return "none";
  }
  
  public Direction GetBeatDirection(int beat){
    int beatIndex = beat - startBeat;
    if (beatIndex < 0) {
      return Direction.NONE;
    }
    if (beatIndex >= actionQueue.Count) {
      return Direction.NONE;
    }
    return actionQueue[beatIndex];
  }

  // Render a grid with the correct inputs selected
  public List<List<bool>> RenderDDR(int thisBeat){
    int displayFuture = 4;
    var retval = new List<List<bool>>();
    
    for (int i = 0; i < displayFuture; i++) {
      Direction dir = GetBeatDirection(thisBeat+1+i);
      if (dir == Direction.NONE){
        List<bool> col = new List<bool>();
        col.Add(false);
        col.Add(false);
        col.Add(false);
        col.Add(false);
        retval.Add(col);
      } if (dir == Direction.UP){
        List<bool> col = new List<bool>();
        col.Add(true);
        col.Add(false);
        col.Add(false);
        col.Add(false);
        retval.Add(col);
      } if (dir == Direction.LEFT){
        List<bool> col = new List<bool>();
        col.Add(false);
        col.Add(true);
        col.Add(false);
        col.Add(false);
        retval.Add(col);
      } if (dir == Direction.RIGHT){
        List<bool> col = new List<bool>();
        col.Add(false);
        col.Add(false);
        col.Add(true);
        col.Add(false);
        retval.Add(col);
      } if (dir == Direction.DOWN){
        List<bool> col = new List<bool>();
        col.Add(false);
        col.Add(false);
        col.Add(false);
        col.Add(true);
        retval.Add(col);
      }
    }
    return retval;
  }
  public string RenderDDRString(int thisBeat){
    List<List<bool>> grid = RenderDDR(thisBeat);

    /*
    U U U U  
    L L L L 
    R R R R
    D D D D
    ^
    */
    string finalString = "\n▼";
    // loop over each letter (row)
    for (int i = 0; i < 4; i++){
      finalString += '\n';
      
      for (int j = 0; j < 4; j++) {
        bool isThere = grid[j][i];
        var displayChar = 'X';
        switch(i) { 
          case 0:
            displayChar = 'U';
            break;
          case 1:
            displayChar = 'L';
            break;
          case 2:
            displayChar = 'R';
            break;
          case 3:
            displayChar = 'D';
            break;
        }
        
        finalString += isThere ? displayChar : ' ';
        finalString += ' ';

      }
      
    }
    finalString += "\n▲";
    finalString += "";
    return finalString;

  }
}
