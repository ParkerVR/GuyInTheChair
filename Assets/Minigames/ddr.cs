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
    actionQueue=new List<Direction>();
    for (int i = 0; i < 8; i++) {
      actionQueue.Add(Direction.NONE);
    }
    enableArrowGuides();
    Hits = 0;
    Misses = 0;
    startBeat = StartBeat;
    beatSum= BeatSum;
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

  public static void enableArrowGuides() {
    var target = GameObject.FindGameObjectWithTag("ddr").GetComponent<Transform>();
    for (int i = 0; i < 4; i++) {
      var arrow = target.transform.GetChild(i);
      var arrowObject = arrow.GetChild(4).gameObject;
      var arrowComponent = arrowObject.GetComponent<SpriteRenderer>();
      arrowComponent.color = new Color(255,114,225);
    }
  }
  public static void disableDDR(){
    var target = GameObject.FindGameObjectWithTag("ddr").GetComponent<Transform>();
    for (int i = 0; i < 4; i++) {
      var arrow = target.transform.GetChild(i);
      for (int j = 0; j < 5; j++) {
        var arrowObject = arrow.GetChild(j).gameObject;
        var arrowComponent = arrowObject.GetComponent<SpriteRenderer>();
        arrowComponent.color = Color.clear;
      }
    }
  }

  // Call this every frame
  public string HitBeatProcess(string keypress, int currentBeat){
    Direction dirThisBeat = GetBeatDirection(currentBeat+1);
    Debug.Log(dirThisBeat);
    if (dirThisBeat != Direction.NONE) {
      if (keypress != "NO INPUT") {
        Debug.Log($"Direction: {dirThisBeat}, keyPress = {keypress}");
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
    Debug.Log(beatIndex);
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

  public void RenderDDRArrows(int thisBeat){
    List<List<bool>> grid = RenderDDR(thisBeat);
    disableDDR();
    enableArrowGuides();

    var target = GameObject.FindGameObjectWithTag("ddr").GetComponent<Transform>();

    // loop over each arrow 
    for (int i = 0; i < 4; i++){
      var arrow = target.transform.GetChild(i);

      // Go into their time beats
      for (int j = 0; j < 4; j++) {
        // Individual arrow pieces
        var arrowObject = arrow.GetChild(j).gameObject;
        var arrowComponent = arrowObject.GetComponent<SpriteRenderer>();

        if (grid[j][i]) {
          arrowComponent.color = Color.white;
        }
      }
    }
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
