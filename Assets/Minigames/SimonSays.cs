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

  public Timer t;
  public int numberOfBeats; // this grows from 1 to maxNumberBeats during the simon says
  public int maxNumberBeats; // how long the game is
  public int speed; // Relative to BPM

  //Plays simon says for the next x beats
  public SimonSays(int MaxNumberBeats, int Speed){
    maxNumberBeats=MaxNumberBeats;
    speed = Speed;
  }


  

  

}
