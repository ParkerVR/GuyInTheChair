using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Timer {
  public readonly float timeSet;
  public float timeLeft;
  public bool timerOn;
  public Timer(float setTime) {
    timeSet = setTime;
    timeLeft = setTime;
    timerOn = true;
  }

  public string getTimeString(){
    if (timeLeft > 0) {
      float minutes = Mathf.FloorToInt(timeLeft / 60);
      float seconds = Mathf.FloorToInt(timeLeft % 60);

      return string.Format("{0:00} : {1:00}", minutes, seconds);
    } else {
      return "0:00";
    }
  }

  public void iterateTimer() {
    if(timerOn) {
      if (timeLeft > 0) {
        timeLeft -= Time.deltaTime;
      } else {
        timeLeft = 0;
        timerOn = false;
      }
    }
  } 
}