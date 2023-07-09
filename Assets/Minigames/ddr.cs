using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDR
{
  public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
  };

  public int numberOfBeats;
  public List<(string, int)> actionQueue;
  //Plays simon says for the next x beats
  public DDR(int NumberOfBeats){
    numberOfBeats=NumberOfBeats;
    actionQueue=new List<(string,int)>();
  }
  public void Update(ArrayList keypresses, int currentBeat){
    //// SIMPLER VERSION ////
    // More simple implementation to decrease penalties and not need to worry about debounce

    // Check action queue and get all keys due on the current beat.  Check if the current input matches what is due
    // If it matches exactly (e.g. only up and down pressed when up and down due), then yippee and drop from queue
    // If it does not match exactly, apply a penalty
    // If any were missed (due on an earlier beat), drop from queue and REE

    //// END SIMPLER VERSION ////

    
    // iterate through all actions in the queue

    // if any are due this beat, compare to pressed keys
    // flash green or something if hit on time and drop from queue

    // if they don't match to anything due this do we want to compare against the ones due on the next beat? 
    // If so, apply a minor penalty and drop them from the queue

    // if any are due even later, apply a penalty for bad keypress but keep in queue

    // if they are due on on earlier beat (missed), drop from queue and apply a penalty

    // From the set timeline, add the string action and the "due" integer value
    
    foreach((string, int) actionPair in actionQueue){
      if(actionPair.Item2 == currentBeat){
        return;
        // prefect
      }

    }
  }


}
