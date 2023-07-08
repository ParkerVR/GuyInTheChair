using System.Collections;
using System.Collections.Generic;

using UnityEngine;

// Generally uses seconds
public class Timer {

  public bool timerOn;
  public float timeTotal; 
  
  public float beatLength;
  
  public static setBeatLength(float bpm) {
    beatLength = 60 / bpm; 
  } 


  public bool wasLastFrameBeat;
  public float lastBeatTime;
  public readonly int beatMax;
  public int totalBeats;

  public Timer(float bpm, int beatOffSet) {
    timeLeft = setTime;
    timerOn = true;
    lastBeatTime = 0;
    setBeatLength(bpm);
    beatMax = beatOffSet;
    totalBeats = 0;
    wasLastFrameBeat = true;
  }

  public string getTimeString(){
    if (timeLeft > 0) {
      float minutes = Mathf.FloorToInt(timeTotal / 60);
      float seconds = Mathf.FloorToInt(timeTotal % 60);

      return string.Format("{0:00} : {1:00}", minutes, seconds);
    } else {
      return "0:00";
    }
  }

  // Returns % drift to the nearest beat the current frame is, rounding to 0 for perfects
  public float iterateTimer() {
    if(timerOn) {
      float retVal = calculateDriftPercentage();
      timeTotal += Time.deltaTime;
      // Just resetting flag after beats.
      if (wasLastFrameBeat) {
        wasLastFrameBeat = false;
      } else {
        // PERFECT! this is a beat frame.
        if (lastBeatTime > beatLength*totalBeats) {
          wasLastFrameBeat = true;
          totalBeats += 1;
          return 0;
          retVal = 0;
        }
      }
      return retVal;
    }
    return -1;
  }

  // max drift percentage is .50 (50%)
  private float calculateDriftPercentage() {
    float driftSeconds = lastBeatTime-beatLength*totalBeats;
    float driftPercentage = driftSeconds / beatLength;
    if (driftPercentage < 0.5) {
      return driftPercentage;
    } else {
      return 1-driftPercentage;
    }
  }
 
}